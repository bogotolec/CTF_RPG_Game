import socket
import threading
from threading import Thread
import random

serv_sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM, proto=0)
serv_sock.bind(('', 6969))
serv_sock.listen(100)

def task(client_sock, client_addr):
    def get_num():
        try:
            num = int(client_sock.recv(1024).decode('utf-8').strip())
            print(num, " from ", client_addr)
            return num
        except Exception as err:
            print(err " from ", client_addr)
            client_sock.close()
            return
        
    def send_text(ans):
        ans = bytes(ans + '\n', encoding="utf-8")
        client_sock.send(ans)
        
    flag_int = random.randint(-2147483648, 2147483647)
    print(flag_int, " from ", client_addr)
    score = "{0}You have {1} attempts"
    send_text(score.format('I forgot my lovely int. Can you help me to remember?\n', '33'))
    
    for i in range(33, 0, -1):
        num = get_num()
        if not num and num != 0:
            break
        if num > flag_int:
            ans = score.format('>, your number is bigger\n', str(i-1))
        elif num < flag_int:
                ans = score.format('<, your number is smaller\n', str(i-1))
        elif num == flag_int:
            send_text('CRG{algoritm_that_everyone_must_know}')
            print(client_addr, " solved")
            break
        send_text(ans)
    else:
        send_text('bad')
        print(client_addr, " failed")
    
    client_sock.close()
    return
    

while True:
    client_sock, client_addr = serv_sock.accept()
    print("Connected from", client_addr)
    Thread(name=client_addr, target=task, args=[client_sock]).start()

