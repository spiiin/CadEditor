using CadEditor;

public class Script
{
    public void Execute(FormScript formScript)
    {
        var log = formScript.getLog();
        log.AppendText("Hello world!\n");
    }
}