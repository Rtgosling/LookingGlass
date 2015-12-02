#pragma once

#include "Sources.h"

enum Tag_type
{
    classic_,
    ultralight_,
    error_
};

class Adafruit_PN532;

// Mifare ISO14443A NFC tags
class Card_data
{
public:
    Card_data ( Adafruit_PN532& nfc_r );
    ~Card_data();

    bool read_card_data();

    Tag_type get_tag_type();
    uint8_t* get_card_data();

private:
    Tag_type set_tag_type();
    uint8_t* set_card_data();

    bool read_classic   ();
    bool read_ultralight();


    Adafruit_PN532& nfc_r_;

    Tag_type tag_type_;
    uint8_t* card_id_p_;
    uint8_t* card_data_p_;

    uint8_t card_id_[ 7 ];
    uint8_t card_id_size_;

    uint8_t card_id_classsic_[ 4 ];
    uint8_t card_data_classic_[ 16 ];

    uint8_t card_id_ultralight_[ 16 ];
    uint8_t card_data_ultralight_[ 32 ];
};