var card_schemas = require( './models/Card' );
var ObjectId     = require( 'mongoose' ).Types.ObjectId;

module.exports = function( app )
{
    app.get( '/', function( req, res )
    {
        /*Card.find( {}, function( err, cards )
        {
            // Return every card in the database
            res.send( cards )
        } );*/
    } );
    
    app.get( '/admin', function( req, res ) {
        res.render( 'admin.ejs' )
    } );
    
    app.post( '/admin/create', function( req, res ) {
       var type = req.body.type;
       
       //console.log( req.body );
       
       switch( type )
       {
            case "land":
                var new_land = new card_schemas.land( {
                    id:     req.body.id,
                    name:   req.body.name,
                    colour: req.body.colour
                } );
                
                new_land.save( function( err ) {
                    if( err ) res.json( { 'error': err } );
                    else res.redirect('/admin');
                } );
                
                break;
            case "creature":
                var new_creature = new card_schemas.creature( {
                    id:          req.body.id,
                    name:        req.body.name,
                    colour:      req.body.colour,
                    type:        req.body.type,
                    description: req.body.description,
                    cost:        req.body.cost,
                    attack:      req.body.attack,
                    defence:     req.body.defence
                } );
                
                new_creature.save( function( err ) {
                    if( err ) res.json( { 'error': err } );
                    else res.redirect('/admin');
                } );
            
                break;
            case "spell":
                var new_spell = new card_schemas.spell( {
                    id:          req.body.id,
                    name:        req.body.name,
                    colour:      req.body.colour,
                    type:        req.body.type,
                    description: req.body.description,
                    cost:        req.body.cost
                } );
                
                new_spell.save( function( err ) {
                    if( err ) res.json( { 'error': err } );
                    else res.redirect('/admin');
                } );
                break;
            default:
                res.json( { 'error': "Invalid request" } );
                break;
       }       
        
    } );
};
