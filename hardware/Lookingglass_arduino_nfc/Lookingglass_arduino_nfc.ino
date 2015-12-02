#include "Sources.h"

void setup()
{
    Lookingglass Lookingglass;
    Lookingglass.~Lookingglass();

#ifdef DEBUG
    Serial.print( "Halting..." );
#endif

    while( true )
    {
        delay( 500 );
    }
}

void loop()
{}
