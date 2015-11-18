var mongoose = require( 'mongoose' );
var bcrypt   = require( 'bcrypt-nodejs' );


var card_schema = mongoose.Schema( {
    id:          String, // Change this to search by ID instead
    name:        String,
    colour:      String,
    type:        String,
    description: String,
    values: {
        cost:    Number,
        attack:  Number,
        defense: Number
    }    
} );

module.exports = mongoose.model( 'card', card_schema );
