internal static class Program {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
        var demo = new Synth.Demo();


        // Produce white noise for 10 seconds
        var startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < 10) { } 
        demo.Stop();
        
    }
}
