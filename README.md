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
