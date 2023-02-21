Aplikacja dla nadleśnictwa do zarządzania opiekowanym lasem. Z założenia powinno być dodawanie i usuwanie rodzajów drzew w modelu Drzewo, dodawanie typów zwierząt(model Zwierze) i oznaczanie ich czy są zagrożone czy nie oraz zmieniania ich ilości w stadzie. Dodatkowko model Pozwolenie gdzie zamieszczane będą ogłoszenia leśniczego, np. pozwolenie na polowania czy zakaz zbierania grzybów.

Założenia:
1. Edycja modeli w administracja, dla każdego osobny kontroller
2. Formularze oparte o bootstrap
3. Edycja modeli jako: dodaj, zmień, usuń
4. Akcje w każdym kontrolerze administracji: Lista, Detale, Dodaj, Edytuj, Usun
5. Kontroller API do każdego z administracyjnych
6. możliwe robienie zmian przez API przez postmana
7. Struktura przepływu danych: Controler -> repozytorium -> bazadanych
8. Wgranie (Seed bazy danych) testowych rekordów do bazy, po 10 rekordów każdego modelu.
9. Migracje baz danych + baza danych na SqLite
10. ** baza danych na MSSQl lub MySQL
11. ** dostęp do administracji po zalogowaniu
12. ** Struktura przepływu danych: Controler -> serwis -> repozytorium -> bazadanych

Model Drzewa:

    Id,
    Data dodania,
    Nazwa,
    Opis,
    Opis liścia,
    Maksymalna wysokość

Model Zwierze:

    Id,
    Data dodania,
    Nazwa,
    Opis,
    wielkość stada,
    czy jest zagrożone

Model Pozwolenia:

    Id,
    Data dodania,
    Tytuł,
    Opis,
    Od kiedy obowiązuje,
    Do kiedy obowiązuje,