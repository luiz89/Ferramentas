
$caminho = FileSelectFolder ("caminho saida client", "K:");
MsgBox(true, "teste", $caminho )

$package = InputBox("package","digite o nome do package")
MsgBox(true, "package", $package )

$wsdl = InputBox("WSDL","digite o caminho do wsdl")
MsgBox(true, "WSDL", $wsdl )

;$comando = "wsimport -s C:\Users\lzcdoss\Desktop\testeGerarClientJava -extension -verbose -keep -p br.com -wsdllocation http://localhost:8787/webService/WebService?wsdl http://localhost:8787/webService/WebService?wsdl -B-XautoNameResolution"
$comando =  "wsimport -s " & $caminho & " -extension -verbose -keep -p " &  $package & " -wsdllocation " & $wsdl &" "& $wsdl & " -B-XautoNameResolution"

local $processo  = cmd('Gerador Stubs Java Wen Service', $comando)

Sleep(15000)

ProcessClose($processo)

Func cmd($sTitle, $sCommand) ; Returns PID of Run.
	Return Run(@ComSpec & " /K title " & $sTitle & "|" & $sCommand)
EndFunc   ;==>_RunCMDe