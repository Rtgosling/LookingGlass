#include "Lookingglass.h"

#define PN532_SCK  (2)
#define PN532_MOSI (3)
#define PN532_SS   (4)
#define PN532_MISO (5)

Adafruit_PN532 nfc(PN532_SCK, PN532_MISO, PN532_MOSI, PN532_SS);

Lookingglass::Lookingglass() :
    nfc_p_      ( 0 ),
    card_data_p_( 0 )
{
    Serial.begin( 115200 ); // Open up the serial port at 115200 baud

#ifdef DEBUG
    Serial.println( "Initialising PN532..." );
#endif

    // Define the NFC object with its respected pins
    Nfc_pins nfc_pins;
    //nfc_p_ = new //Adafruit_PN532( nfc_pins.serial_clk, nfc_pins.miso, nfc_pins.mosi, nfc_pins.slave_select );

    /*if( !init_pn532() )
    {
#ifdef DEBUG
        Serial.println( "Error: couldn't initialise PN532!" );
#endif
        return;
    }

    process_cards();*/
}

/*Lookingglass::~Lookingglass()
{
    delete card_data_p_;
    delete nfc_p_;
}

// Attempt to intialise the PN532 chip
bool Lookingglass::init_pn532()
{
    nfc_p_->begin();

    uint32_t version_data = nfc_p_->getFirmwareVersion();

    if( !version_data )
    {
        return false;
    }

#ifdef DEBUG
    Serial.print  ( "Found chip PN53" );
    Serial.println( ( version_data >> 24 ) & 0xFF, HEX );
    Serial.print  ( "Firmware ver. " );
    Serial.print  ( ( version_data >> 16 ) & 0xFF, DEC );
    Serial.print  ( '.' );
    Serial.println( ( version_data >> 8 ) & 0xFF, DEC );
#endif

    nfc_p_->SAMConfig();

    return true;
}

// Read and handle card data
void Lookingglass::process_cards()
{
    while( true )
    {
        bool success = false;
        
        card_data_p_ = new Card_data( *nfc_p_ );

        if( !card_data_p_->read_card_data() )
        {
            continue; // Reset read if nothing is read
        }

        output_card_data();
    }
}

void Lookingglass::output_card_data()
{
    uint8_t* data = card_data_p_->get_card_data();

#ifdef DEBUG
    Serial.println( *data );
#endif    
    
    // Send to Node.js client over serial
}*/
