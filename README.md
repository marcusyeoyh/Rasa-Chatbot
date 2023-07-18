[![Repository Status](https://img.shields.io/badge/Repository%20Status-Maintained-dark%20green.svg)](https://github.com/marcusyeoyh)
[![Author](https://img.shields.io/badge/Author-Marcus%20Yeo-blue.svg)](https://www.linkedin.com/in/AVS1508/)
[![Latest Release](https://img.shields.io/badge/Latest%20Release-18%20July%202023-yellow.svg)](https://github.com/marcusyeoyh/Rasa-Chatbot/commit/master)

# Rasa ChatBot

This Repo is to store progress for a conversational chatbot which queries a SQLite database using the Rasa framework

## Operation instructions
### Operating Full WPF App
1. Run actions server for custom actions
```
rasa run actions
```
2. Run Rasa REST API to allow connection from WPF App
```
rasa run -m <path_to_your_model> --enable-api --cors "*" //For regular use
rasa run -m <path_to_your_model> --enable-api --cors "*" --debug //For debugging purposes
```
3. Run WPF App
* Make sure that server endpoint is correct

### Testing Rasa Backend
1. Run actions server
```
rasa run actions
```
2. Run Rasa Chatbot
```
rasa shell //Run this to view the chatbot in terminal
rasa shell --debug //run this to view debug mode in terminal
```
## Contents: [C# WPF Frontend](https://github.com/marcusyeoyh/Rasa-Chatbot/tree/main/BaseWPFApp)
* This folder contains the WPF Frontend connected with the developed [Rasa Chatbot Backend](https://github.com/marcusyeoyh/Rasa-Chatbot/tree/main/default)
## Contents: [Rasa Chatbot Backend](https://github.com/marcusyeoyh/Rasa-Chatbot/tree/main/default)
### [Actions Folder](https://github.com/marcusyeoyh/Rasa-Chatbot/tree/main/default/actions)

This folder contains the implementations for custom actions. 
* The implementation can be found in the [actions.py](https://github.com/marcusyeoyh/Rasa-Chatbot/blob/main/default/actions/actions.py) file.

### [Data Folder](https://github.com/marcusyeoyh/Rasa-Chatbot/tree/main/default/data)

This folder contains the main implementations of the Chatbot.
* The [nlu.yml](https://github.com/marcusyeoyh/Rasa-Chatbot/blob/main/default/data/nlu.yml) file contains the implementations for the NLU portion of the chatbot. Here, intents such as greet and ask_product are implemented to give the bot test cases to identify certain intents of the user.
* The [rules.yml](https://github.com/marcusyeoyh/Rasa-Chatbot/blob/main/default/data/rules.yml) file represents the pre-set rules for user inputs. Rules ensure a certain flow of events will occur upon certain user inputs.
* The [stories.yml](https://github.com/marcusyeoyh/Rasa-Chatbot/blob/main/default/data/stories.yml) file contains the various use cases that are anticipated by the user. These give the chatbot a guide to structure conversations and allows for custom inputs to be crafted.


### [DB Folder](https://github.com/marcusyeoyh/Rasa-Chatbot/tree/main/default/db)

This folder contains the implementation of a mock database to be queried by the chatbot. The database has been implemented in SQLite and is edited using a [database editor](https://sqlitebrowser.org/)

* The [connectdb.ipynb](https://github.com/marcusyeoyh/Rasa-Chatbot/blob/main/default/db/connectdb.ipynb) file represents a python jupyter notebook which contains the testing code for the database to ensure that data can be retrieved and is of working order.
* The [database1.db](https://github.com/marcusyeoyh/Rasa-Chatbot/blob/main/default/db/database1.db) file contains the implementation of the database in SQLite

### [Models Folder](https://github.com/marcusyeoyh/Rasa-Chatbot/tree/main/default/models)

This folder contains the trained models for the chatbot.
Once the files have been downloaded, the chatbot can be trained by running the following command while in the file.
```
rasa run action
rasa shell
```

### [config.yml](https://github.com/marcusyeoyh/Rasa-Chatbot/blob/main/default/config.yml)

This file contains the implementation for the display and Memoization policy of the chatbot

### [credentials.yml](https://github.com/marcusyeoyh/Rasa-Chatbot/blob/main/default/credentials.yml)

This file contains the implementations for external implementations

### [domain.yml](https://github.com/marcusyeoyh/Rasa-Chatbot/blob/main/default/domain.yml)

This file contains the implementations for the responses and actions that the bot can provide a user. 

This file also contains the various slots and entities that are required for the bot to identify in order to execute certain functions.

## TODO

1. Implement working staff and customer modes with different privileges afforded to both accounts

## References
* [Rasa Documentation](https://rasa.com/docs/)
* [Querying database with Rasa Chatbot](https://www.youtube.com/watch?v=iyfJ0jx87w0&t=149s&ab_channel=Rasa)

