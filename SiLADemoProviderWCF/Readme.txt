SiLADemoProvider
Visual Studio 2010

Provides a WCF Service for Incubator device

Use Visual Studio to start SiLADemoProvider like self hosting service. 
Set Command Line Argument "-debug" for SiLAHost project and set it to be StartUp Project.

Run SiLADemoProvider service like Windows Service. 
Build SiLADemoProviderSetup installer project and run the installer.

Settings:

 InstrumentName - the name of demo instrument (InX55)

 Port - 65432 by default
 ServiceName - SiLAWebService
 Server - localhost

http://localhost:65432/sila

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