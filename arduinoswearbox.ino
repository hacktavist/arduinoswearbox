#include <Apex5400BillAcceptor.h>
//Bill acceptor library courtesy of:
//Jesse Campbell, http://www.jbcse.com/
//Bill acceptor library: https://github.com/hackwin/ArduinoPyramidApex5000BillAcceptor
//Requires library: http://gammon.com.au/Arduino/ReceiveOnlySoftwareSerial.zip

String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete
String commandString = "";
int numberOfForbiddenWords  = 0;
String forbiddenWordsCommand = "";
int cumulativeWords = 0;


boolean needsCredits = false;
int credits = 0;

#define PIN_ENABLE 7 //Purple wire, to enable set low, to disable set high
#define PIN_INTERRUPT_LINE 8 //Orange wire on Apex, Request to send data to host
#define PIN_SEND_LINE 9 //White/Blue wire, Host Ready Signal
#define PIN_TTL_RX 10 //Green wire, Transmit Data Line from acceptor
#define PULSE_PIN 12
Apex5400BillAcceptor *billAcceptor;
int code;


void setup() {

  Serial.begin(9600);
  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(PULSE_PIN, OUTPUT);
  billAcceptor = new Apex5400BillAcceptor(PIN_ENABLE, PIN_INTERRUPT_LINE, PIN_SEND_LINE, PIN_TTL_RX);
  billAcceptor->disable();
}

void loop() {

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
      Serial.println("# of words: " + String(numberOfForbiddenWords));
      cumulativeWords += numberOfForbiddenWords;
      Serial.println("Cumulative: " + String(cumulativeWords));

      if(credits < cumulativeWords){
        needsCredits = true;
        billAcceptor->enable();
      } else {
        Serial.println("Subtracting cum");
        credits -= cumulativeWords;
        cumulativeWords -= numberOfForbiddenWords;
      }
    }


    inputString = "";
  }
  if(needsCredits){
    Serial.println("Need some money bitch");
    checkMoney();
  }

}

void checkMoney(){
  if(code = billAcceptor->checkForBill()){
      if(billAcceptor->getDollarValue(code) != NULL){
        Serial.print("Code: 0x");
        Serial.print(code, HEX);
        Serial.print(", Description: ");
        Serial.println(billAcceptor->getDescription(code));
        credits += billAcceptor->getDollarValue(code);
        Serial.println("credits: " + String(credits));
              if(credits >= cumulativeWords){
                credits -= cumulativeWords;
                billAcceptor->disable();
                cumulativeWords = 0;
                needsCredits = false;
              }
      }
  }
  pulse(PULSE_PIN,1);
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

void pulse(int pin, int pulses)
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
