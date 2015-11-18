var Card     = require( './models/Card' );
var ObjectId = require( 'mongoose' ).Types.ObjectId;

module.exports = function( app ) {
    app.get( '/', function( req, res ) {
        Card.find( {}, function( err, cards ) {
            var returned_cards = [];
            
            for( var card_iter = 0; card_iter < cards.length; card_iter++ )
            {
                returned_cards.push( cards[ card_iter ] );
                returned_cards[ card_iter ]._id = undefined;
            }
                        
            // Return everything in the database
            for( var card_iter = 0; card_iter < cards.length; card_iter++ )
            res.json( {
                card: cards[ card_iter ]
            } );
        } );
    } );
};