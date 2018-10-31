

String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete
String commandString = "";


boolean isConnected = false;



void setup() {
  
  Serial.begin(9600);
  pinMode(LED_BUILTIN, OUTPUT);

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
  else if(commandString.equals("FRBD")){
    digitalWrite(LED_BUILTIN, HIGH);
  }
  else if(commandString.equals("FINE")){
    digitalWrite(LED_BUILTIN, LOW);
  }
  
  inputString = "";
}

}




void getCommand()
{
  if(inputString.length()>0)
  {
     commandString = inputString.substring(1,5);
  }
}

void turnLedOn(int pin)
{
  digitalWrite(pin,HIGH);
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

