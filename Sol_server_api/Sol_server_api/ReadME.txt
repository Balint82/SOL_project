>A tokenkulcs generált, így a szerverújraindításkor változik
>A munkatárs adatainak frissítésénél email alapján azonosítja az adatbázisban a keresett rekord-ot

Jelen helyzet szerint a regisztrációt az Admin végzi.
A Coworker táblán keresztül regisztrálja majd a PersonalData táblát, és a Login Táblát is abban kell majd egy Register metódust írni ami ezeket
elvégzi a videó alapján.

Nem lesz a Login-nek POST metódusa, csak majd GET ami belépésnál összehasonlítja a kapott API-n keresztül a mentett login-t és a kapott login-t

Json Coworker teszt:
{
  "coworkerID": 0,
  "coworkerName": "Jane Doe",
  "personalData": {
    "telNumber": "+9876543210",
    "email": "jane.doe@example.com",
    "address": "456 Elm St, Othertown, OT",
    "coworkerID": 0
  },
  "roleID": 2,
  "plkLoginID": 0,
  "login": {
    "loginID": 0,
    "loginName": "janedoe",
     "password": "password123",
    "fkLoginCWID": 0
  }
}

2.
{
  "coworkerID": 123,
  "coworkerName": "John Doe",
  "personalData": {
    "telNumber": "+123456789",
    "email": "john.doe@example.com",
    "address": "123 Main Street, Anytown"
  },
  "roleID": 2,
  "loginDTOObj": {
    "loginName": "johndoe123",
    "password": "password123"
  }
}

3.
{
  "coworkerName": "Michael Smith",
  "personalData": {
    "telNumber": "+987654321",
    "email": "michael.smith@example.com",
    "address": "456 Oak Avenue, Springfield"
  },
  "roleID": 1,
  "loginDTOObj": {
    "loginName": "msmith789",
    "password": "Password!"
  }
 }

 4.
 {
  "coworkerName": "Emily Johnson",
  "personalData": {
    "telNumber": "+1122334455",
    "email": "emily.johnson@example.com",
    "address": "789 Elm Street, Pleasantville"
  },
  "roleID": 3,
  "loginDTOObj": {
    "loginName": "ejohnson987",
    "password": "SecurePass987!"
  }
}


Továbbfejlesztés:
Admin regisztráció MFA-val(2FA)


Feladatok:
- Megoldani hogy oké gombbal tűnjön el a gomb és vele az értesítő üzenet
- Megoldani, hogy a db-ben egyedien lehesssen csak tárolni az e-mail címeket.
- Megoldani, hogy a Projekt dátum mezője csak a napig tárolja a dátumot.

Tesztadat:
1- Szakember

tom
pw123

trin
pw123

pityu
pw123


Hátralévő funkciók:
Specialist:
    - Kiválasztott alkatrészek projekthez rendelése
    - Alkatrészek listázása
    - Becsült mukavégzési idő rögzítése, árkalkuláció
    - Projekt lezárás


    1. Alkatrészhez rekesz hozzárendelése
    2. Kiválasztott alkatrész projekthez rendelésekor vonja le a valósmennyiséget a rekeszben tárolt mennyiségből, ha kisebb a rekeszben tárolt mennyiség tájéékoztasson és mondja el mennyire van még szükség.