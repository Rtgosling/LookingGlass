#include "Looking_glass.h"

void setup()
{
    Looking_glass looking_glass;

    Serial.println( "Error: Nfc_test.ino broke free of looking_glass!");
    Serial.print  ( "Halting..." );

    looking_glass.~Looking_glass();

    for(;;);
}


void loop()
{
    // Nothing should get here
}
