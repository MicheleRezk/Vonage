# Contact Centre application

This service used to handle interactions coming to the vonage contact center.

# Business logic

There are 3 levels of employees: Agents, Supervisors, General Managers:
● There can be multiple Agents.
● There can be multiple Supervisors, but not more than Agents.
● There can be only one General Manager.
There are 2 types of interactions: voice (phone), non-voice (text chat) with interaction routing protocols as
follows:
● An Agent can handle 1 voice or 2 non-voice interactions at a time.
● A Supervisor can handle 1 voice or 2 non-voice interactions at a time.
● The General Manager can handle 1 interaction at a time.
The contact center allocates the incoming interactions in a specific way.

1. An incoming interaction needs to be allocated to an Agent.
2. If he cannot handle (reached maximum of interactions at a time) the interaction, it must be
   forwarded to Supervisor.
3. In case the Supervisor cannot handle the interaction, it needs to be forwarded further, this time
   to the General Manager.
4. If no-one can handle the interaction, the strategy is to reject that interaction.

# How to test it

## 2 ways to test the apis:

- Using VS and Swagger UI
- running docker-compose up and access services from postman (baseUrl: http://localhost:9070)

## Handle Interaction api

you can access the service using this url: /api/interactions
this is a post method, you can then pass the type of the interaction as below:

```json
{
  "type": "Voice"
}
```

or

```json
{
  "type": "NonVoice"
}
```

## response

this is sample of the responses:

```json
{
  "response": "Agent_1 is handling the Voice interaction",
  "status": "Running",
  "handledBy": "Agent"
}
{
    "response": "There is no free employee can handle this Voice interaction now",
    "status": "Rejected",
    "handledBy": null
}
```

# how to configure the app

you can configure the service settings in Vonage.ContactCenter.appsettings.json file

```json
"ServiceSettings": {
    "AgentsNumber": 3, // number of Agents
    "SupervisorsNumber": 2, // number of supervisors
    "AverageVoiceInteractionInMilliseconds": 240000, // voice interaction take 4 mins to be handled
    "AverageNonVoiceInteractionInMilliseconds": 120000 // voice interaction take 2 mins to be handled
  }
```

## Solution architecture and technologies used

Solution consists of 3 projects:

- Backend "Vonage.ContactCenter" which is .Net6 web api
- Vonage.ContactCenter.Tests contains unit tests using xunit
- Vonage.ContactCenter.IntegrationTests contains integration tests using xunit
