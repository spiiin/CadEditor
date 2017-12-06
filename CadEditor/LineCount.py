# -*- coding: cp1251 -*-
import os,string
import sys
 
extension_list = ['h','hpp','cpp','c','pas','dpr','asm','py','q3asm','def','sh','bat','cs','java','cl','lisp','ui',"nut"]
comment_sims   = {'asm' : ';', 'py'  : '#', 'cl':';','lisp':';'}
 
source_files= { }
 
if len(sys.argv)!=2:
    print "you must call 'LineCount.py <path_to_folder>'"
    raw_input()
    exit(-1)
path = sys.argv[1]
 
files_count = 0
def calc_files_count (arg,dirname, names):
  global files_count
  files_count+=len(names)
 
 
def calc_strings_count(arg, dirname, names):
    #print "%32s"%dirname
    for name in names:
        full_name = os.path.join(dirname,name)
        file_name,file_ext = os.path.splitext(full_name)
        file_ext = file_ext[1:].lower()
        if comment_sims.has_key(file_ext):
          comment_sim  = comment_sims[file_ext]
        else:
          comment_sim  = "//"
        if file_ext in extension_list:
            if file_name.lower().find(".designer") != -1:
                continue
            if full_name.find("IronPythonLib") != -1:
                continue
            f = file(full_name)
            file_text = f.readlines()
            empty_lines_count = 0
            comment_lines = 0
            for line in file_text :
              line_without_spaces = line.lstrip(string.whitespace)
              if line_without_spaces=="":
                empty_lines_count += 1
              elif line_without_spaces.startswith(comment_sim):
                comment_lines +=1
            source_files[full_name]= {"full" : len(file_text) ,"empty" :empty_lines_count, "comment":comment_lines}
            f.close()
 
def calc(path_root):
    os.path.walk (path_root,calc_files_count,0)
    print "Found %i files"%files_count
    print ""
 
    # рекурсивно посчитать кол-во строк
    os.path.walk(path_root,calc_strings_count,0)
 
    #перевести в список, отсортировать и вывести на экран
 
    lst = source_files.items()
    lst.sort(key = lambda (key, val): val["full"])
 
    strings_count=0
    empty_lines_count=0
    comment_lines_count=0
    #for name,val in source_files.iteritems():
    for name,val in lst:
        l_f,l_e,l_c = val["full"],val["empty"],val["comment"]
        dummy,short_name = os.path.split(name)
        print "%-36s   : %5i (%i/%i/%i)"%(short_name,l_f, l_f-l_c-l_e,l_c,l_e )
        strings_count+=l_f
        empty_lines_count+=l_e
        comment_lines_count+=l_c
    print "\nformat -\nfilename  : full_lines_count (code_lines_count/comments_count/empty_lines_count)"
    print 24*"-"
    print "Summary : %4i lines"%strings_count
    print ""
    print "Code    : %4i lines"%(strings_count - comment_lines_count - empty_lines_count)
    print "Comments: %4i lines"%comment_lines_count
    print "Empty   : %4i lines"%empty_lines_count
    print 24*"-"
print "%s %s %s"%( "="*24, "================", "="*24)
print "%-24s %s %24s"%( "="*3, "Spin LineCounter", "="*3)
print "%s %s %s"%( "="*24, "================", "="*24)
calc(path)
raw_input()