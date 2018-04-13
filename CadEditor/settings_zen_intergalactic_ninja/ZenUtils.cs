using CadEditor;
using System;

public static class ZenUtils 
{ 
    public static GetPalFunc readPalFromBin(string[] fname)
    {
        return (int x)=> { return Utils.readBinFile(fname[x]); };
    }
    
    public static GetVideoPageAddrFunc fakeVideoAddr()
    {
        return (int _)=> { return -1; };
    }
    
    public static GetVideoChunkFunc getVideoChunk(string[] fname)
    {
       return (int x)=> { return Utils.readVideoBankFromFile(fname[x], 0); };
    }
}