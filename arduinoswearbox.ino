#define shock 7

String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete
String commandString = "";
int numberOfForbiddenWords  = 0;
String forbiddenWordsCommand = "";


boolean isConnected = false;



void setup() {
  
  Serial.begin(9600);
  pinMode(shock, OUTPUT);

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
    pulseLed(shock, numberOfForbiddenWords);
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
