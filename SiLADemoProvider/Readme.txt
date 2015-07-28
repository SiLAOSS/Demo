SiLADemoProvider
Visual Studio 2010

Provides a Web Service for Incubator device

Use Visual Studio to start ASP.NET Development Server

or run from command protmpt:

cd C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0
WebDev.WebServer40.EXE /port:65021 /path:"C:\dev\Code Samples\SiLAOSS\SiLADemo\SiLADemoProvider"

Settings:

 InstrumentName - the name of demo instrument (InX55)

 Port - 65021 by default
 ServiceName - SiLAWebService
 Server - localhost

   http://localhost:65021/SiLAWebService.asmx


 LogFileName - a simple text logfile (%Temp%\SiLADemoLog.log)

 DemoStepDuration - 1000
 DemoStepsCount - 20
  
   The demo device executes 20 steps when running DoContinue method.
   Each step takes 1000 ms.


Sample data for SetParameters method:

<?xml version="1.0" encoding="utf-8"?>
<ParameterSet>
   <Parameter name="Temperature">
    <Int32>33</Int32>
  </Parameter>
  <Parameter name="TimeInSeconds">
    <Int32>1500</Int32>
  </Parameter>
</ParameterSet>