# obd2-reader
A Simple OBD-II reader

This is to be used on an Raspberry Pi with a Display connected in order to OBD-II data to guages on the display. 

I am currently using https://github.com/Ircama/ELM327-emulator in order to emulate a car. Along with http://com0com.sourceforge.net/ to run a null serial modem on my machine so that I can connect the emulator and the application to virtual serial ports.

Also, I will give a shoutout to https://github.com/DarthAffe/OBD.NET for a fantastic library that allows me to have a good starting point in reading the obd2 data coming in via the elm327 device.
