#pragma once

#include <Arduino.h>
#include <PN532.h>

class Looking_glass
{
public:
    Looking_glass ();
    ~Looking_glass();

private:
    bool init_pn532();
    bool read_nfc_tag();

    unsigned int Pn532_cs_;
    PN532*       nfc_p_;
};
