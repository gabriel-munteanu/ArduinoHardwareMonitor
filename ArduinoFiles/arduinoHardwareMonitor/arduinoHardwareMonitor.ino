#include <CmdMessenger.h>
#include <LiquidCrystal.h>

CmdMessenger cmdMessenger = CmdMessenger(Serial);
LiquidCrystal lcd(7, 6, 5, 4, 3, 2);
const int buttonNext = 8;

// Commands
enum
{  
  pCheckArduino,
  aCheckResponse,
  pSendValues,
  pSendEmptyValues,
  aGoNextSensor
};

void attachCommandCallbacks()
{ 
  cmdMessenger.attach(pCheckArduino, OnCheckArduino);
  cmdMessenger.attach(pSendValues, OnReceiveValues);
  cmdMessenger.attach(pSendEmptyValues, OnReceiveNothing);
}

void OnCheckArduino()
{
  cmdMessenger.sendCmd(aCheckResponse,"OK");
  OnReceiveNothing();
}

void OnReceiveNothing()
{
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd.print("No sensors");
  lcd.setCursor(0, 1);
  lcd.print("checked!");
}

void OnReceiveValues()
{
  char* firstLine = cmdMessenger.readStringArg();
  char* secondLine = cmdMessenger.readStringArg();
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd.print(firstLine);
  lcd.setCursor(0, 1);
  lcd.print(secondLine);
}

void setup() 
{   
  Serial.begin(115200); 
  lcd.begin(16, 2);
  cmdMessenger.printLfCr();
  attachCommandCallbacks(); 
  
  //configure pin 8 to use a button 
  pinMode(buttonNext, INPUT);
  digitalWrite(buttonNext, HIGH); 
 
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd.print("Waiting to start");
  lcd.setCursor(0, 1);
  lcd.print("AHM"); 
}

int lastButtonState = true;

void loop() 
{  
  // Process incoming serial data, and perform callbacks
  cmdMessenger.feedinSerialData(); 
  
  int buttonState = digitalRead(buttonNext);  
  if(buttonState == false && lastButtonState == true)
  {
    cmdMessenger.sendCmd(aGoNextSensor);
    lastButtonState = buttonState;
  }
  
  if(buttonState == true && lastButtonState == false)
    lastButtonState = buttonState;
  
}
