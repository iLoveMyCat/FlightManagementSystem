Authentication:
I chose JWT for stateless authentication to make the system easier to scale across instances.

Database Access:
All interactions with the database go through stored procedures. This helps encapsulate business logic at the DB level, simplifies future changes, and adds an extra layer of control and security.

Indexing Strategy:
Since alert matching is a performance-critical operation, I planned for compound indexes on Origin, Destination, and FlightTime to support fast lookups.

De-duplication Logic:
Handling duplicates really depends on the format and frequency of the external price feed. For now, I assume each API has its own way of identifying unique flight offers.

Push Notifications:
I assumed use of a service like Firebase for push notifications, each user can register a device token, which is stored in the system and used to send alerts once a price match occurs.
