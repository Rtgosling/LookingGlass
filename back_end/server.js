var express      = require( 'express' );
var app          = express();
var port         = process.env.PORT || 8080;
var mongoose     = require( 'mongoose' );
var morgan       = require( 'morgan' );
var cookieParser = require( 'cookie-parser' );
var bodyParser   = require( 'body-parser' );
var session      = require( 'express-session' );

var configDB = require( './config/database.js' );

// Aquire database configuration
mongoose.connect( configDB.url );

app.use( function( req, res, next ) {
    req.db = mongoose;
    next();
} );
app.use( morgan( 'dev' ) ); // Log every request to the console
app.use( cookieParser() );  // Read cookies for auth
app.use( bodyParser.urlencoded( { extended: false } ) ); // Get information in html forms
app.use( session( { secret: 'itsasecrettoeverybody' } ) );

require( './routes.js' )( app );

var server = app.listen( port, '0.0.0.0', function() {
    console.log( 'Listening on port %d', server.address().port );
} );
