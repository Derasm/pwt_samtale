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

Backend:
- Repository pattern
- unit of work pattern
- simulation af køb.
- unit test
- TLS
- JWT
- data validering

Frontend:
- TLS
- JWT
- Fremvisning af data i en overskuelig måde.
- Cart.
- Vurdér hvor meget der skal kunne søges på (Pris, Størrelse, Længde? (if any), Navn, sæson?)
- Filter
- Søgning
- Frontend test. 