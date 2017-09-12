open('encrypted_image', 'w').write(' '.join([str(y ^ ord(' ')) for y in [ord(x) for x in open('base64_flag').read()]])[::-1])
# base64  french_flag.jpg > base64_flag; python3 -c "open('encrypted_image', 'w').write(' '.join([str(y ^ ord(' ')) for y in [ord(x) for x in open('base64_flag').read()]])[::-1])"
# base64  pirate_flag.jpg > base64_flag; python3 -c "open('encrypted_image', 'w').write(' '.join([str(y ^ ord(' ')) for y in [ord(x) for x in open('base64_flag').read()]])[::-1])"
# base64  cat_flag.png > base64_flag; python3 -c "open('encrypted_image', 'w').write(' '.join([str(y ^ ord(' ')) for y in [ord(x) for x in open('base64_flag').read()]])[::-1])"
