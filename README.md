# Geolocalization API

## How to run the API on your computer with Docker
    
    cd 'path/To/GeolocalizationApi/Solution'

    docker build -t 'landr:geolocalizationapi' .
    docker run -it -p 5050:80 --name gelocalizationapi landr:geolocalizationapi

## Online Documentation : Swagger

    http://localhost:5050/

## Examples:

#### GET caller IP geolocation

    GET http://localhost:5050/api/Geolocalization

#### POST Batch of IPs

    POST http://localhost:5050/api/Geolocalization
    {
        "IpAddressList": ["192.168.0.0","128.0.0.0", "1.0.0.0"]
    }
    
