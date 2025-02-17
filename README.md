# pwt_samtale



Process:
1. Kig på database, se om man kan få hoved og hale i skema.
2. Byg op bagfra: EF -> db-first -> API.
3. Byg API
4. Byg frontend
5. lav tests af backend
6. lav tests af frontend

## Database:

- varer.SupplierNo -> beholdning.SupplierID
- varer.EAN -> beholdning.ean -- have a relationship
- ShopID: has no constraint, also not an identity. Uniqueness enforced code-side?
- Lookup table: Beholdning has a relationship with varer.

Backend:
TODO: 
1. appsettings [x]
2. connectionstring [x]
3. EF db first [x]
4. JWT
5. TLS
6. DTO's.

- Repository pattern [Unnecessary - EF handles]
- unit of work pattern [Unnecessary - EF handles]
- simulation af køb. [out of scope?]
- unit test
- TLS
- JWT
- data validering

Frontend:
- TLS
- JWT
- Fremvisning af data i en overskuelig måde.
- Filter
- Søgning
- Cart.
- Vurdér hvor meget der skal kunne søges på (Pris, Størrelse, Længde? (if any), Navn, sæson?)

- Frontend test. 