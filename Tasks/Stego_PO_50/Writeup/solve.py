with open("Edgar's_poem.txt") as input_file:
    text = input_file.read()
    flag = ""
    for char in text:
        if char.isupper():
            flag += char
    print(flag)
