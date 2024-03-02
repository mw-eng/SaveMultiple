# SaveMultiple
Macro to automatically save multiple channels, screens, traces, SnP.
## Requirements / Supported Platforms
* .NET Framework 4.0 or later.
* VNA A.17.20.09 (64 bit)

## Version Log
### 1.0.0.0 - 2024/03/02
* First release.
### 1.0.0.1 - 2024/03/02
* Modifying assembly information

## How to use
### Installing / Uninstalling Macro
#### Installing
Save the "SaveMultiple.exe" file in a suitable directory.

#### Uninstalling
Delete the saved "SaveMultiple.exe" file.  
Recommended storage location is  
*C:\Users\Public\Documents\Network Analyzer*  
or  
*C:\Program Files\Keysight\Network Analyzer\Service*
### Macro registration procedure
Screen|Description
---|:--
<img src="https://github.com/mw-eng/SaveMultiple/blob/master/assets/MacroRegistration_1.png?raw=true" width="250px">|Select *Macro* button/ (I)<br><br>Select *Macro Setup...* button. (II)
<img src="https://github.com/mw-eng/SaveMultiple/blob/master/assets/MacroRegistration_2.png?raw=true" width="450px">|For the *Macro Title:* , choose an empty title or a changeable title. (III)<br>\*Button doesn't show last line?<br><br>Select *Edit...* button. (IV)
<img src="https://github.com/mw-eng/SaveMultiple/blob/master/assets/MacroRegistration_3.png?raw=true" width="450px">|Enter the name to be displayed on the *Macro Button* in the Macro Title* field. (V)<br><br>In the *Macro Executable* filed, enter the address of the *Save Multiple.exe* file you saved. (VI)<br>\*You can also select *Macro Executable* fields using the *Browse...* button.
<img src="https://github.com/mw-eng/SaveMultiple/blob/master/assets/MacroRegistration_4.png?raw=true" width="450px">|Verify that the macro has been added (or changed) and select *OK* button. (VII)

### How to use macro
Screen|Description
---|:--
<img src="https://github.com/mw-eng/SaveMultiple/blob/master/assets/MacroUse_1.png?raw=true" width="250px">|Select the registered macro button. (A)
![MacroUse_2](https://github.com/mw-eng/SaveMultiple/blob/master/assets/MacroUse_2.png?raw=true)|(B) Select target channel.<br>\*Only *SnP* and *Auto Single Trigger* are valid.<br><br>(C) Select to automatically execute a *Single Trigger* befor data acquisition.<br><br>(D) Select target data.<br><br>(E) Select target *Test Port*.<br>\*Only if *SnP* is the target.<br><br>(F) Enter a header for the safe file name.<br>\*A file with *"\_CHx", "\_Sheetx"*, etc. appended to the name specified here will be output.<br><br>(G)Start the save process.
![MacroUse_3](https://github.com/mw-eng/SaveMultiple/blob/master/assets/MacroUse_3.png?raw=true)|After selecting the output destination, select *OK* button to start the output process. (B)
