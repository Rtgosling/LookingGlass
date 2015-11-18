#include "Looking_glass.h"
#include <SPI.h>

namespace
{
    const uint8_t total_blocks_ = 45;
    const uint8_t block_size_   = 16;

    bool reading_ = false;
}

Looking_glass::Looking_glass() :
    Pn532_cs_( 10 ),
    nfc_p_   ( 0 )
{
    Serial.begin( 115200 );

    nfc_p_ = new PN532( Pn532_cs_ );

    if( !init_pn532() )
    {
        Serial.println( "Error: couldn't initialise PN532!" );
        Serial.print  ( "Halting..." );
        while( true );
    }

    reading_ = true;

    while( reading_ )
    {
        read_nfc_tag();
    }
}

Looking_glass::~Looking_glass()
{}

bool Looking_glass::init_pn532()
{
    nfc_p_->begin();

    uint32_t version_data = nfc_p_->getFirmwareVersion();

    if( !version_data )
    {
        return false;
    }

    Serial.print  ( "Found chip PN53" );
    Serial.println( ( version_data >> 24 ) & 0xFF, HEX );
    Serial.print  ( "Firmware ver. " );
    Serial.print  ( ( version_data >> 16 ) & 0xFF, DEC );
    Serial.print  ( '.' );
    Serial.println( ( version_data >> 8 ) & 0xFF, DEC );
    Serial.print  ( "Supports " );
    Serial.println( version_data & 0xFF, HEX );

    nfc_p_->SAMConfig();

    return true;
}

bool Looking_glass::read_nfc_tag()
{
    uint32_t nfc_id = nfc_p_->readPassiveTargetID( PN532_MIFARE_ISO14443A );

    if( nfc_id != 0 )
    {
        Serial.println();
        Serial.println( "Read card #" );
        Serial.print  ( nfc_id );
        Serial.println();

        // Generate a default key for a new card
        uint8_t keys[] = { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

        // Each block must be authenticated
        for( uint8_t block_num = 0; block_num < total_blocks_; block_num++ )
        {
            if( nfc_p_->authenticateBlock( 1, nfc_id, block_num, KEY_A, keys ) )
            {
                uint8_t block[ block_size_ ];

                if( nfc_p_->readMemoryBlock( 1, block_num, block ) )
                {
                    // Print contents of each memory block
                    for( uint8_t block_iter = 0; block_iter < block_size_; block_iter++ )
                    {
                        Serial.println( block[ block_iter ], HEX );
                        
                        // Data beautification
                        if( block[ block_iter ] <= 0xF )
                        {
                            Serial.print( "  " );
                        }
                        else
                        {
                            Serial.print( " " );
                        }
                    }

                    Serial.print( "| block " );

                    // Data beautification
                    if( block_num <= 9 )
                    {
                        Serial.print( " " );
                    }

                    Serial.print( block_num, DEC );
                    Serial.print( " | " );

                    if( block_num == 0 )
                    {
                        Serial.println( "Manufacturer Block" );
                    }
                    else if( ( ( block_num + 1 ) % 4 ) == 0 )
                    {
                        Serial.println( "Sector Trailer" );
                    }
                    else
                    {
                        Serial.println( "Data Block" );
                    }
                }
            }
        }
    }

    delay( 500 );

    return true;
}