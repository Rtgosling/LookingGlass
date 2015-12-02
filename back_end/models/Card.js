var mongoose = require( 'mongoose' );
var extend   = require( 'mongoose-schema-extend' );
var bcrypt   = require( 'bcrypt-nodejs' );

var card_schema = mongoose.Schema( {
    id:          String,
    name:        String,
    colour:      String
} );

    var land_card = card_schema.extend( {
        // Nothing to add
    } );

    var playing_card = card_schema.extend( {
        type:        String,
        description: String,
        cost:        Number
    } );

        var creature_card = playing_card.extend( {
            attack:  String,
            defense: String
        } );

        var spell_card = playing_card.extend( {
            // Nothing to add
        } );

module.exports = {
    land:     mongoose.model( 'land_card', land_card ),
    creature: mongoose.model( 'creature_card', creature_card ),
    spell:    mongoose.model( 'spell_card', spell_card )
};