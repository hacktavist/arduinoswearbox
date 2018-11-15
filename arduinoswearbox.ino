#include <Apex5400BillAcceptor.h>
//bill acceptor library courtesy of jesse campbell; requires receive only software serial library
//https://github.com/hackwin/ArduinoPyramidApex5000BillAcceptor
String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete
String commandString = "";
int numberOfForbiddenWords  = 0;
String forbiddenWordsCommand = "";


boolean isConnected = false;

#define PIN_ENABLE 7 //Purple wire, to enable set low, to disable set high
#define PIN_INTERRUPT_LINE 8 //Orange wire on Apex, Request to send data to host
#define PIN_SEND_LINE 9 //White/Blue wire, Host Ready Signal
#define PIN_TTL_RX 10 //Green wire, Transmit Data Line from acceptor

Apex5400BillAcceptor *billAcceptor;
int code;


void setup() {
  
  Serial.begin(9600);
  
  pinMode(LED_BUILTIN, OUTPUT);
  billAcceptor = new Apex5400BillAcceptor(PIN_ENABLE, PIN_INTERRUPT_LINE, PIN_SEND_LINE, PIN_TTL_RX);    
}

void loop() {
if(code = billAcceptor->checkForBill()){
    Serial.print("Code: 0x");
    Serial.print(code, HEX);
    Serial.print(", Description: ");
    Serial.println(billAcceptor->getDescription(code));
  }
if(stringComplete)
{
  stringComplete = false;
  getCommand();
  
  if(commandString.equals("STAR")){
    digitalWrite(LED_BUILTIN, HIGH);
  }
  if(commandString.equals("STOP")){
    digitalWrite(LED_BUILTIN, LOW);   
  }
  else if(commandString.equals("FRBD"+String(numberOfForbiddenWords))){
    Serial.println("We have the forbidden command string");
    //do some stuff with the bill acceptor
  }
  
  inputString = "";
}

}




void getCommand()
{
  if(inputString.length()>0)
  {
     commandString = inputString.substring(1,5);
     Serial.println(commandString.indexOf("FRBD"));
     if(commandString.indexOf("FRBD") >= 0){
      forbiddenWordsCommand = commandString;
      numberOfForbiddenWords = inputString.substring(5,6).toInt();
      commandString = forbiddenWordsCommand+String(numberOfForbiddenWords);
      Serial.println("# of words: "+String(numberOfForbiddenWords));
      Serial.println("cmd string: "+forbiddenWordsCommand);
     }
  }
}

void pulseLed(int pin, int pulses)
{
  for(int i=0; i < pulses; i++){
    digitalWrite(pin,LOW);
    delay(1000);
    digitalWrite(pin,HIGH);
    delay(1000);
  }
}

void turnLedOff(int pin)
{
  digitalWrite(pin,LOW);
}

void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    char inChar = (char)Serial.read();
    // add it to the inputString:
    inputString += inChar;
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == '\n') {
      stringComplete = true;
    }
  }
}
