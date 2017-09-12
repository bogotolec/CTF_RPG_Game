import socket

sock = socket.socket()
sock.connect(('162.212.152.75', 6969))
l = -2147483648
r = 2147483647
text = sock.recv(1024).decode('utf-8')
print(text)
for i in range(33):
    find_num = (r + l)//2
    sock.send(bytes(str(find_num), encoding='utf-8'))
    text = sock.recv(1024).decode('utf-8')
    print(find_num, i)
    print(text)
    if text[0] == '>':
        r = find_num
    elif text[0] == '<':
        l = find_num
    else:
        break
print(text)
