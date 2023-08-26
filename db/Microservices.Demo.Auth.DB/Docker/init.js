db = connect("localhost:27017/SecurityDB");
db.createCollection('Users');
db.Users.insertMany([
    {
        "Username": "erick",
        "Password": "erick",
        "FirstName": "Erick",
        "LastName": "Aróstegui Cunza",
        "Avatar": "assets/avatars/erick.png",
        "AvailableProducts": [
            "TRI",
            "HSI",
            "FAI",
            "CAR"
        ]
    },
    {
        "Username": "eva",
        "Password": "eva",
        "FirstName": "Eva",
        "LastName": "Levano Guerra",
        "Avatar": "assets/avatars/eva.png",
        "AvailableProducts": [
            "TRI",
            "HSI",
            "FAI",
            "CAR"
        ]
    },
    {
        "Username": "oscar",
        "Password": "oscar",
        "FirstName": "Oscar",
        "LastName": "Donayre Aróstegui",
        "Avatar": "assets/avatars/oscar.png",
        "AvailableProducts": [
            "TRI",
            "HSI",
            "FAI",
            "CAR"
        ]
    }
])