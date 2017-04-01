;Downloads .Net 2.0 Framework if it isn't installed.
;Based on code found at: http://priyank.co.in/install-dot-net-framework-using-inno-setup
;Added checks for 64 bit windows. Only windows XP 64 and Server 2003 64 are relevant for this
;because .Net 2.0 comes preloaded on Vista and above.
[Files]
Source: {#SourceFiles}\isxdl.dll; DestDir: {tmp}; Flags: deleteafterinstall

[Code]
var
    dotnetRedistPath: string;
    downloadNeeded: boolean;
    dotNetNeeded: boolean;

procedure isxdl_AddFile(URL, Filename: PChar);
external 'isxdl_AddFile@files:isxdl.dll stdcall';
function isxdl_DownloadFiles(hWnd: Integer): Integer;
external 'isxdl_DownloadFiles@files:isxdl.dll stdcall';
function isxdl_SetOption(Option, Value: PChar): Integer;
external 'isxdl_SetOption@files:isxdl.dll stdcall';

const
  dotnetRedistURL = 'http://download.microsoft.com/download/5/6/7/567758a3-759e-473e-bf8f-52154438565a/dotnetfx.exe';
  dotnetRedistURL64 = 'http://download.microsoft.com/download/a/3/f/a3f1bf98-18f3-4036-9b68-8e6de530ce0a/NetFx64.exe';
function InitializeSetup(): Boolean;
var
  IsInstalled: Cardinal;
begin
  Result := true;
  dotNetNeeded := true;

  // Check for required netfx installation
  if (RegValueExists(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v2.0.50727', 'Install')) then
  begin
    RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v2.0.50727', 'Install', IsInstalled);
    if(IsInstalled = 1) then
    begin
      dotNetNeeded := false;
      downloadNeeded := false;
    end;
  end;


  if(dotNetNeeded) then
  begin
    if (not IsAdminLoggedOn()) then
    begin
      MsgBox('The Microsoft .NET Framework needs to be installed by an Administrator.', mbError, MB_OK);
      Result := false;
    end
	else
    begin
	  if IsWin64 then
	  begin
        dotnetRedistPath := ExpandConstant('{src}\netfx64.exe');
      end
      else
      begin
        dotnetRedistPath := ExpandConstant('{src}\dotnetfx.exe');
      end;
    end;
    if not FileExists(dotnetRedistPath) then
    begin
	  if IsWin64 then
	  begin
        dotnetRedistPath := ExpandConstant('{tmp}\dotnetfx.exe');
      end
      else
      begin
        dotnetRedistPath := ExpandConstant('{tmp}\netfx64.exe');
      end;
      if not FileExists(dotnetRedistPath) then
      begin
        if IsWin64 then
        begin
          isxdl_AddFile(dotnetRedistURL64, dotnetRedistPath);
        end
        else
        begin
          isxdl_AddFile(dotnetRedistURL, dotnetRedistPath);
        end;
        downloadNeeded := true;
      end;
    end;
  end;
end;

function NextButtonClick(CurPage: Integer): Boolean;
var
  hWnd: Integer;
  ResultCode: Integer;
begin
  Result := true;

  if CurPage = wpReady then
  begin
    hWnd := StrToInt(ExpandConstant('{wizardhwnd}'));

    // don't try to init isxdl if it's not needed because it will error on < ie 3
    if (downloadNeeded) then
    begin
      isxdl_SetOption('label', 'Downloading Microsoft .NET 2.0 Framework');
      isxdl_SetOption('description', 'Please wait while setup installs the Microsoft .NET 2.0 Framework.');
      if isxdl_DownloadFiles(hWnd) = 0 then Result := false;
    end;
    if (Result = true) and (dotNetNeeded = true) then
    begin
      if Exec(ExpandConstant(dotnetRedistPath), '', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then
      begin
        // handle success if necessary; ResultCode contains the exit code
        if not (ResultCode = 0) then
        begin
          Result := false;
        end;
      end
      else
      begin
        // handle failure if necessary; ResultCode contains the error code
        Result := false;
      end;
    end;
  end;
end;
