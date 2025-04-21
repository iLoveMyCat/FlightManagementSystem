Data Flow: User Creates Alert to Notification

1. User creates an alert through the mobile application by sending a POST request to the Alert API containing alert preferences (origin, destination, and flight time).
2. Alert API processes the request by first checking if the user exists in the database. If not, the user is created automatically. The alert is then stored in the database.
3. External price feed sources periodically send updated flight pricing data to the system.
4. Price Processor receives flight price data, normalizes it, and queries the database to find matching alerts based on origin, destination, and flight time.
5. If a matching alert is found, the processor retrieves the user's device token and constructs a push notification payload.
6. Notification Service sends the push notification to the user’s mobile device using a push delivery platform like Firebase or APNs.
7. User receives a push notification on their device and can open the app to view more flight details.
