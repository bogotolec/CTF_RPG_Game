with open("output") as file:
    reversed_xored_nums = file.read()
    xored_nums = reversed_xored_nums[::-1].split(' ')
    nums = [ord(" ") ^ int(num) for num in xored_nums]
    base64_string = ''.join([chr(x) for x in nums])
    open("qwer", 'w').write(base64_string)
    
