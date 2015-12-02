#pragma once

#include "Sources.h"

struct Nfc_pins
{
    const uint8_t serial_clk   = 2;
    const uint8_t miso         = 3;
    const uint8_t slave_select = 4;
    const uint8_t mosi         = 5;
};

class Card_data;
class Adafruit_PN532;

class Lookingglass
{
public:
    Lookingglass ();
    ~Lookingglass();

private:
    bool init_pn532();

    void process_cards   ();
    void output_card_data();

    Adafruit_PN532* nfc_p_;
    Card_data*      card_data_p_;
};
