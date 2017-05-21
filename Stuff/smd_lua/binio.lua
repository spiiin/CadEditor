-- version 0.1.2
BinIO = {}
BinIO.File = nil
-- fileNameOrHandle: string or file handle, file name to open or file handle to set.
-- [mode]: string, open mode, always in binary mode, see io.open.
-- return filehandle: file handle, return nil when operation fail
function BinIO.Open(fileNameOrHandle, mode)
	if( mode == nil ) then mode = "rb" end
	local filehandle = nil
	if( mode:sub(-1) ~= "b" ) then mode = mode.."b" end -- Always in binary mode
	if( type(fileNameOrHandle) == "string" ) then -- File name
		BinIO.File = io.open(fileNameOrHandle, mode)
		filehandle = BinIO.File
	elseif( io.type(fileNameOrHandle) == "file" ) then
		io.input(fileNameOrHandle)
		BinIO.File = fileNameOrHandle
		filehandle = BinIO.File
	end
	return filehandle
end

-- [fileHandle]: file handle, file handle to close.
function BinIO.Close(fileHandle)
	if( fileHandle == nil ) then fileHandle = BinIO.File end
	if( io.type(fileHandle) == "file" ) then
		io.close(fileHandle)
	end
	if( fileHandle == BinIO.File ) then BinIO.File = nil end -- In case of unwanted modify of closed file handle.
end

-- val: object, value converting to binary string, can be string, number.
-- [length]: number, how many bytes to convert, default 1. 0x00 was fiiled to empty bytes. Only last [length] bytes are used.
-- return byteList : byte table, converted byte[] array.
function BinIO.ToByteList(val, length)
	local byteList = {}
	local valType = type(val)
	if( valType == "number" ) then
		local BYTE_MASK  = 0xFF
		local BYTE_SHIFT = 0x100
		if( length == nil ) then length = 1 end
		for i = 1, length do
			byteList[length + 1 - i] = bit.band( val, BYTE_MASK )
			val = math.floor( val / BYTE_SHIFT )
		end
	elseif( valType == "string" ) then
		for i = 1, length do
			local char = val:sub(i, i)
			if( char ~= "" ) then
				byteList[i] = string.byte(char)
			else
				byteList[i] = 0
			end
		end
	end
	return byteList
end

-- val: object, value to write as binary. Can be signed number, bool(0 = false, other = true), string (encoded in ASCII only, sadly) or byte[] array. Number should be 0x0 ~ 0xFFFFFFFF(-2147483648~2147483647), or data loss will occurred in division, however number within range 0x0 ~ 0xFFFFFFFFFFFF seems works too. "wb" mode is required for string, if not, 0x0A will become 0x0D 0x0A, that'll ruin everything. If the val is a table, it must be a byte[] array, or it will be ignored.
-- [length]: number, how many bytes to write, 0x00 was fiiled to empty bytes, default 1(for bool) or string.len(val)(for string) or proper length for number or #val(for bytes table). Only last [length] bytes are used for number, only the first [length] bytes are used for stirng and table.
-- [fileHandle]: file handle, file to operate. Default is BinIO.File.
-- return size : number, how many bytes are used theoretically, return 0 when no writing or failed to write.
function BinIO.Write(val, length, fileHandle)
	local size = 0
	if( fileHandle == nil ) then fileHandle = BinIO.File end
	if( val ~= nil and fileHandle ~= nil ) then
		local isByteArray  = false
		local writeAll     = false
		local byteList     = {}
		local byteStr      = ""
		local valType      = type(val)
		if( valType == "string" ) then -- String can be write directly
			if( length == nil ) then length = val:len() end
			byteStr = val:sub(1, length)
			isByteArray  = false
		elseif( valType == "number" ) then
			local absVal = val
			if( length == nil ) then
				if( val < 0 ) then absVal = -1 - val end
				length = math.floor( ( math.log(absVal) / math.log(2) + 1 ) / 8 ) + 1
			end
			byteList = BinIO.ToByteList(val, length)
			isByteArray  = true
		elseif( valType == "boolean" ) then
			if( length == nil ) then length = 1 end
			if( val ) then
				byteStr = string.char(1)
			else
				byteStr = string.char(0)
			end
			if( length > 1) then
				byteStr = string.rep(string.char(0), length - 1)..byteStr
			end
			isByteArray  = false
		elseif( valType == "table" ) then
			isByteArray  = true
			if( length == nil ) then
				writeAll = true
				length = 1 -- This should not be set, but I've no idea why (writeAll or (size < length)) will throw error when length is nil
			end
		else
			isByteArray  = false
		end 
		if( isByteArray ) then 
			if( valType == "table" ) then -- Performance enhance by table reuse
				for i = 1, #val do
					if( writeAll or (size < length) ) then
						byteList[i] = string.char(val[i])      -- No check for better performance
						size = size + 1
					else
						break
					end
				end
			else
				for i = 1, #byteList do
					if( writeAll or (size < length) ) then
						byteList[i] = string.char(byteList[i]) -- No check for better performance
						size = size + 1
					else
						break
					end
				end
			end
			byteStr = table.concat(byteList)
		end
		local byteStrLength = byteStr:len()
		if( byteStrLength < length ) then
			if( valType == "string" ) then  -- String are stored from the high end, for it can be read by simple file:read()
				byteStr = byteStr..string.rep(string.char(0), length - byteStrLength)
			else
				byteStr = string.rep(string.char(0), length - byteStrLength)..byteStr
			end
			size = size + length - byteStrLength
		end
		fileHandle:write(byteStr)
	end
	return size
end

-- length: number, length in bytes to read. If it's larger than file length, return all read data. Only the last byte is used for "bool" [readType]
-- [readType]: string, what kind of data should treat readed bytes as, can be "b"(default, return byte[]) or "n"(return number) or "s"(return string) or "bool"(return true/false)
-- [fileHandle]: file handle, file to operate. Default is BinIO.File.
-- return data: byte table/number/bool/string, read byte[] array or signed number or string, depends on [readType]. Return nil if fail or exceed file length. Return empty table/0/nil/empty string if length is 0.
function BinIO.Read(length, readType, fileHandle)
	local data = {}
	if( fileHandle == nil ) then fileHandle = BinIO.File end
	if( readType == nil ) then readType = "b" end
	if( fileHandle ~= nil ) then
		if( readType == "s" ) then -- String can be read directly
			data = fileHandle:read(length)
		else
			local byteChar = nil
			for ptr = 1, length do
				byteChar = fileHandle:read(1)
				if( byteChar == nil ) then
					data = nil
					break
				else
					table.insert(data, string.byte(byteChar))
				end
			end
		end
	else
		data = nil
	end
	if( data ~= nil and readType ~= "b" and readType ~= "s") then -- Convert the result
		local len = #data
		if( readType == "n" ) then
			local num = 0
			if( next(data) ~= nil ) then
				local BYTE_MASK = 0xFF
				local BYTE_SIZE = 0x100
				local sign = bit.band(data[1], 0x80)
				if( sign == 0 ) then
					for i = 1, len do
						num = num + data[len - i + 1] * BYTE_SIZE^(i-1)
					end
				else
					for i = 1, len do
						num = num + (BYTE_MASK - data[len - i + 1]) * BYTE_SIZE^(i-1)
					end
					num = -num - 1
				end
			end
			data = num
		elseif( readType == "bool" ) then
			local bool = nil
			if( len > 0 ) then
				bool = not (data[len] == 0)
			end
			data = bool
		end
	end
	return data
end

-- [whence]: string, sets and gets the file position, see file:seek
-- [offset]: number, sets and gets the file position, see file:seek
-- [fileHandle]: file handle, file to operate. Default is BinIO.File.
-- return pos: number, return the current position, return nil if fail
function BinIO.Seek(whence, offset, fileHandle)
	local pos = nil
	if( fileHandle == nil ) then fileHandle = BinIO.File end
	if( fileHandle ~= nil ) then
		pos = fileHandle:seek(whence, offset)
	end
	return pos
end

-- mode: buffering mode for file handle, see file:setvbuf
-- [size]: buffering handle, only functional for "full" and "line" mode, see file:setvbuf
function BinIO.SetBuffer(mode, size)
	if( fileHandle == nil ) then fileHandle = BinIO.File end
	if( fileHandle ~= nil ) then
		fileHandle:setvbuf(mode, size)
	end
end