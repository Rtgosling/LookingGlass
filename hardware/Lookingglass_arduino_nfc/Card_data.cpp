#include "Card_data.h"

namespace
{
    const uint8_t classic_block_size_   = 4;
    const uint8_t ultralight_page_size_ = 4;

    int size_of_array( uint8_t* input_array )
    {
        return sizeof( input_array ) / sizeof( input_array[ 0 ] );
    }
}

Card_data::Card_data( Adafruit_PN532& nfc_r ) :
    nfc_r_       ( nfc_r ),
    tag_type_    ( set_tag_type() ),
    card_data_p_ ( set_card_data() ),
    card_id_size_( 0 )
{
    for( int iter = 0; iter < size_of_array( card_id_ ); iter++  )
    {
        card_id_[ iter ] = 0;
    }
}

Card_data::~Card_data()
{}

Tag_type Card_data::set_tag_type()
{
    bool id_read_ok = nfc_r_.readPassiveTargetID( PN532_MIFARE_ISO14443A, card_id_, &card_id_size_ );

    if( !id_read_ok )
    {
#ifdef DEBUG
        Serial.println( "Failed to read id!" );
        Serial.println( "Retrying..." );
#endif
        return error_;
    }

#ifdef DEBUG
    // Display NFC ID data
    Serial.println( "" );
    Serial.println( "ISO14443A Tag" );
    Serial.print  ( "ID:   " );
#endif

    nfc_r_.PrintHex( card_id_, card_id_size_ );

#ifdef DEBUG
    Serial.println( "" );
    Serial.print  ( "Size: " );
    Serial.print  ( card_id_size_, DEC );
    Serial.print  ( " bytes" );
    Serial.println( "" );
    Serial.println( "" );
#endif
    
    if( card_id_size_ == size_of_array( card_id_classsic_ ) )
    {
        return classic_;
    }
    else if( card_id_size_ == size_of_array( card_id_ultralight_ ) )
    {
        return ultralight_;
    }

    return error_;
}

uint8_t* Card_data::set_card_data()
{
    if( tag_type_ == classic_ )
    {
        return card_data_classic_;
    }
    else if( tag_type_ == ultralight_ )
    {
        return card_data_ultralight_;
    }
    else
    {
        return NULL;
    }
}

bool Card_data::read_card_data()
{
    if( tag_type_ == classic_ )
    {
        return read_classic();
    }
    else if( tag_type_ == ultralight_ )
    {
        return read_ultralight();
    }

    return false;    
}

bool Card_data::read_classic()
{
    uint8_t key_a[ 6 ] = { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

#ifdef DEBUG
    Serial.println( "Authenticating block..." );
#endif

    bool block_auth_ok = nfc_r_.mifareclassic_AuthenticateBlock( card_id_, card_id_size_, size_of_array( card_id_classsic_ ), 0, key_a );

    if( !block_auth_ok )
    {
#ifdef DEBUG
        Serial.println( "Authentification failed!" );
        Serial.println( "Retrying..." );
#endif
        return false;
    }

#ifdef DEBUG
    Serial.println( "Block authentcated OK!" );
    Serial.println( "Reading block..." );
#endif

    bool block_read_ok = nfc_r_.mifareclassic_ReadDataBlock( size_of_array( card_data_classic_ ), card_data_classic_ );

    if( !block_read_ok )
    {
#ifdef DEBUG
        Serial.println( "Block read failed!" );
        Serial.println( "Retrying..." );
#endif
        return false;
    }

#ifdef DEBUG
    Serial.println( "Read OK!" );
    Serial.print( "Block 4: " );
#endif

    nfc_r_.PrintHexChar( card_data_classic_, classic_block_size_ );

#ifdef DEBUG
    Serial.println( "" );
#endif

    return true;
}

bool Card_data::read_ultralight()
{
#ifdef DEBUG
    Serial.println( "Reading page..." );
#endif

    bool page_read_ok = nfc_r_.mifareultralight_ReadPage( ultralight_page_size_, card_data_ultralight_ );

    if( !page_read_ok )
    {
#ifdef DEBUG
        Serial.println( "Page read failed!" );
        Serial.println( "Retrying..." );
#endif

        return false;
    }

#ifdef DEBUG
    Serial.println( "Read OK!" );
    Serial.print( "Page 4: " );
#endif
    nfc_r_.PrintHexChar( card_data_ultralight_, ultralight_page_size_ );
#ifdef DEBUG
    Serial.println( "" );
#endif

    return true;
}

Tag_type Card_data::get_tag_type()
{
    return tag_type_;
}

uint8_t* Card_data::get_card_data()
{
    return card_data_p_;
}
