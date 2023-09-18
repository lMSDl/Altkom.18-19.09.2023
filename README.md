## CLI
* Nowy projekt
    * konsolowy
    ```
    dotnet new console [-o <LOKALIZACJA> -n <NAZWA_PROEKTU>]
    ```    
    * WebAPI
    ```
    dotnet new webapi [-o <LOKALIZACJA> -n <NAZWA_PROEKTU>] [--no-hhtps]
    ```
    * biblioteki
    ```
    dotnet new classlib [-o <LOKALIZACJA> -n <NAZWA_PROEKTU>]
    ```
* Kompilacja i uruchomienie
    ```
    dotnet build
    dotnet <NAZWA_PROJEKTU>.dll [<PARAMETRY>]
    ```
    ```
    dotnet [watch] run [PARAMETRY]
    ```
* Pakiety i referencje
    * Dodawanie referencji
    ```
    dotnet add reference <ŚCIEŻKA_PROJEKTU>
    ```
