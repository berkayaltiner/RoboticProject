#include "Servo.h" //servo motor library;
#define M1_Zero 95//hardware correction--Range -85 to 85
#define M1_Min -95
#define M1_Max 95
//#define M1_Min 0
//#define M1_Max 180
#define M2_Zero 5//Range -85 to 85
#define M2_Min 0//Right
#define M2_Max 60//Left
Servo M1, M2;
int Th1, Th2, Th3 , tmp;
void setup() 
{
  Serial.begin(9600); // Communication between Arduino and C#
  pinMode(4,OUTPUT);
  digitalWrite(4,0);
  Th1 = 0;
  Th2 = 0;
  M1.attach(2);
  M2.attach(3);             
}

void loop() 
{
  if(Serial.available()>=2)
  {
    Th1 = Serial.read(); // Value that comes from Visual Studio .Net Forms Application
    Th2 = Serial.read(); // Value that comes from Visual Studio .Net Forms Application
    Th3=1;

    while(Serial.available()) tmp = Serial.read();    
    
    if(Th3 ==1) digitalWrite(4,1);
    else digitalWrite(4,1);

      M1.write(Th1);
      M2.write(Th2);
    
    
    
  }


}























//#include "Servo.h"
//Servo M1, M2;
//#define M1_Zero 95 // Range
//#define M1_Min -95              //dereceleri ayarla
//#define M1_Max 95
//
//#define M2_Zero 5 // Range
//#define M2_Min 0              // dereceleri ayarla
//#define M2_Max 60
//
//void Move_Motors(int th1, int th2)
//{
//   if(th1<M1_Min) th1 = M1_Min;
//   if(th1>M1_Max) th1 = M1_Max;
//
//   if(th2<M2_Min) th2 = M2_Min;
//   if(th2>M2_Max) th2 = M2_Max;
//
//   M1.write(M1_Zero + th1);
//   M2.write(M2_Zero + th2);
//}
//
//void setup() {
////  M1.attach(2); // (pin_id)
////  M2.attach (3);
//    pinMode(4, OUTPUT);
//    
////  M1.write(95); // ( and a value from 0 to 180) initial angle.
////  M2.write(5);
//    digitalWrite(4,0);
//}
//
//void loop() {
//  //M1.write(95); // ( and a value from 0 to 180)
//  //M2.write(65);
////  Move_Motors(45,45);
////  delay(2000);
////  
////  Move_Motors(0,0);
////  delay(2000);
////  
////  Move_Motors(-45,45);
////  delay(2000);
////
////  Move_Motors(0,0);
////  delay(2000);
//
//    digitalWrite(4,1);
//    delay(1000);
//    digitalWrite(4,0);
//    delay(1000);
//  
//
//}
