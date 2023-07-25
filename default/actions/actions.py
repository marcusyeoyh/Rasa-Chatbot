import json
import os
from typing import Any, Text, Dict, List
from rasa_sdk import Action, Tracker
from rasa_sdk.executor import CollectingDispatcher
import sqlite3
from sqlite3 import Error
from fuzzywuzzy import process

confidence_rating = 80

config_file = os.path.join(os.path.dirname(__file__), "config.json")
with open(config_file, "r") as file:
    config = json.load(file)
    database_path = config["database_path"]

def is_user_mode_customer(tracker: Tracker) -> bool:
    user_mode = tracker.get_slot("user_mode")
    return user_mode == "Customer"

class QueryBrandType(Action):
    def name(self) -> Text:
        return "query_brand_type"

    def run(
        self,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> List[Dict[Text, Any]]:
        connection = sqlite3.connect(database_path)
        cursor = connection.cursor()

        slot_value = next(tracker.get_latest_entity_values("brand_type"), None)
        slot_name = "Brand"

        # Step 1: Fetch the brand options from the database
        select_query = f"SELECT DISTINCT {slot_name} FROM ProductItem"
        cursor.execute(select_query)
        rows = cursor.fetchall()
        brand_options = [row[0] for row in rows]

        # Step 2: Use fuzzy matching to find the closest matching brand
        closest_match = process.extractOne(slot_value, brand_options)

        if closest_match[1] >= confidence_rating:  # Similarity threshold for a valid match
            slot_value = closest_match[0]
            select_query = f"SELECT * FROM ProductItem WHERE {slot_name} = '{slot_value}'"
            cursor.execute(select_query)
            rows = cursor.fetchall()

            rows_dict = []
            for row in rows:
                row_dict = {description[0]: value for description, value in zip(cursor.description, row)}
                rows_dict.append(row_dict)

            response_data = {"table": rows_dict}
        else:
            response_data = None

        response_json = json.dumps(response_data)

        dispatcher.utter_message(text=response_json)

        # Step 4: Close the connection
        connection.close()

        return []


class QueryAllTransactions(Action):
    def name(self) -> Text:
        return "query_all_transactions"

    def run(
        self,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> List[Dict[Text, Any]]:
        if is_user_mode_customer(tracker):
            dispatcher.utter_message(text="As a customer, you do not have privileged assess to this query.")
        else:
            connection = sqlite3.connect(database_path)
            cursor = connection.cursor()

            select_query = f"SELECT * FROM StoreTransaction"
            cursor.execute(select_query)
            rows = cursor.fetchall()

            rows_dict = []
            for row in rows:
                row_dict = {description[0]: value for description, value in zip(cursor.description, row)}
                rows_dict.append(row_dict)

            response_data = {"table": rows_dict}

            # Convert response_data to JSON string
            response_json = json.dumps(response_data)

            dispatcher.utter_message(text=response_json)

            connection.close()

        return []


class QueryAllProducts(Action):
    def name(self) -> Text:
        return "query_all_products"

    def run(
        self,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> List[Dict[Text, Any]]:
        connection = sqlite3.connect(database_path)
        cursor = connection.cursor()

        select_query = f"SELECT * FROM ProductItem"
        cursor.execute(select_query)
        rows = cursor.fetchall()

        rows_dict = []
        for row in rows:
            row_dict = {description[0]: value for description, value in zip(cursor.description, row)}
            rows_dict.append(row_dict)

        response_data = {"table": rows_dict}

        response_json = json.dumps(response_data)

        dispatcher.utter_message(text=response_json)

        connection.close()

        return []


class CustomActionTest(Action):
    def name(self) -> Text:
        return "custom_action_test"

    def run(
        self,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> List[Dict[Text, Any]]:
        dispatcher.utter_message(text="Custom Action Works!")
        return []

class QueryAllInformation(Action):
    def name(self) -> Text:
        return "query_all_information"

    def run(
        self,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> List[Dict[Text, Any]]:
        if is_user_mode_customer(tracker):
            dispatcher.utter_message(text="As a customer, you do not have privileged assess to this query.")
        else:
            connection = sqlite3.connect(database_path)
            cursor = connection.cursor()

            select_query = f"SELECT * FROM ProductItem, StoreTransaction WHERE ProductItem.ProductID = StoreTransaction.ProductID"
            cursor.execute(select_query)
            rows = cursor.fetchall()

            rows_dict = []
            for row in rows:
                row_dict = {description[0]: value for description, value in zip(cursor.description, row)}
                rows_dict.append(row_dict)

            response_data = {"table": rows_dict}

            # Convert response_data to JSON string
            response_json = json.dumps(response_data)

            dispatcher.utter_message(text=response_json)

            connection.close()

        return []
    
class QueryAllBrands(Action):
    def name(self) -> Text:
        return "query_unique_brands"

    def run(
        self,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> List[Dict[Text, Any]]:
        connection = sqlite3.connect(database_path)
        cursor = connection.cursor()

        # Step 1: Fetch the brand options from the database
        select_query = f"SELECT DISTINCT Brand FROM ProductItem"
        cursor.execute(select_query)
        rows = cursor.fetchall()

        rows_dict = []
        for row in rows:
            row_dict = {description[0]: value for description, value in zip(cursor.description, row)}
            rows_dict.append(row_dict)

        response_data = {"table": rows_dict}

        # Convert response_data to JSON string
        response_json = json.dumps(response_data)

        dispatcher.utter_message(text=response_json)

        connection.close()

        return []
    
class QueryTag(Action):
    def name(self) -> Text:
        return "query_tag"

    def run(
        self,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> List[Dict[Text, Any]]:
        if is_user_mode_customer(tracker):
            dispatcher.utter_message(text="As a customer, you do not have privileged assess to this query.")
        else:
            connection = sqlite3.connect(database_path)
            cursor = connection.cursor()

            slot_value = next(tracker.get_latest_entity_values("tag_type"), None)
            slot_name = "Tag"

            select_query = f"SELECT DISTINCT {slot_name} FROM ProductItem"
            cursor.execute(select_query)
            rows = cursor.fetchall()
            brand_options = [row[0] for row in rows]

            closest_match = process.extractOne(slot_value, brand_options)

            if closest_match[1] >= confidence_rating:  # Minimum similarity threshold for a valid match
                slot_value = closest_match[0]
                select_query = f"SELECT * FROM ProductItem, StoreTransaction WHERE ProductItem.{slot_name} = '{slot_value}' AND StoreTransaction.{slot_name} = '{slot_value}'"
                cursor.execute(select_query)
                rows = cursor.fetchall()

                rows_dict = []
                for row in rows:
                    row_dict = {description[0]: value for description, value in zip(cursor.description, row)}
                    rows_dict.append(row_dict)

                response_data = {"table": rows_dict}
            else:
                response_data = None

            response_json = json.dumps(response_data)

            dispatcher.utter_message(text=response_json)

            connection.close()

        return []
    
class QueryTransactionsTag(Action):
    def name(self) -> Text:
        return "query_transactions_tag"

    def run(
        self,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> List[Dict[Text, Any]]:
        if is_user_mode_customer(tracker):
            dispatcher.utter_message(text="As a customer, you do not have privileged assess to this query.")
        else:
            connection = sqlite3.connect(database_path)
            cursor = connection.cursor()

            slot_value = next(tracker.get_latest_entity_values("tag_type"), None)
            slot_name = "Tag"

            select_query = f"SELECT DISTINCT {slot_name} FROM ProductItem"
            cursor.execute(select_query)
            rows = cursor.fetchall()
            brand_options = [row[0] for row in rows]

            closest_match = process.extractOne(slot_value, brand_options)

            if closest_match[1] >= confidence_rating:  # Minimum similarity threshold for a valid match
                slot_value = closest_match[0]
                select_query = f"SELECT * FROM StoreTransaction WHERE {slot_name} = '{slot_value}'"
                cursor.execute(select_query)
                rows = cursor.fetchall()

                rows_dict = []
                for row in rows:
                    row_dict = {description[0]: value for description, value in zip(cursor.description, row)}
                    rows_dict.append(row_dict)

                response_data = {"table": rows_dict}
            else:
                response_data = None

            response_json = json.dumps(response_data)

            dispatcher.utter_message(text=response_json)

            connection.close()

        return []
    
class QueryProductsTag(Action):
    def name(self) -> Text:
        return "query_products_tag"

    def run(
        self,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> List[Dict[Text, Any]]:
        if is_user_mode_customer(tracker):
            dispatcher.utter_message(text="As a customer, you do not have privileged assess to this query.")
        else:
            connection = sqlite3.connect(database_path)
            cursor = connection.cursor()

            slot_value = next(tracker.get_latest_entity_values("tag_type"), None)
            slot_name = "Tag"

            select_query = f"SELECT DISTINCT {slot_name} FROM ProductItem"
            cursor.execute(select_query)
            rows = cursor.fetchall()
            brand_options = [row[0] for row in rows]

            closest_match = process.extractOne(slot_value, brand_options)

            if closest_match[1] >= confidence_rating:  # Minimum similarity threshold for a valid match
                slot_value = closest_match[0]
                select_query = f"SELECT * FROM ProductItem WHERE {slot_name} = '{slot_value}'"
                cursor.execute(select_query)
                rows = cursor.fetchall()

                rows_dict = []
                for row in rows:
                    row_dict = {description[0]: value for description, value in zip(cursor.description, row)}
                    rows_dict.append(row_dict)

                response_data = {"table": rows_dict}
            else:
                response_data = None

            response_json = json.dumps(response_data)

            dispatcher.utter_message(text=response_json)

            connection.close()

        return []
    

class RedirectPage(Action):
    def name(self) -> Text:
        return "redirect_page"

    def run(
        self,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> List[Dict[Text, Any]]:
        slot_value = next(tracker.get_latest_entity_values("page_number"), None)
        if slot_value:
            page_number = slot_value.lower()
        else:
            page_number = None
        
        connection = sqlite3.connect(database_path)
        cursor = connection.cursor()

        slot_name = "ProductID"

        # Fetch all available product IDs from the database
        select_query = f"SELECT DISTINCT {slot_name} FROM ProductItem"
        cursor.execute(select_query)
        rows = cursor.fetchall()

        # Check if page_number matches any product ID
        if page_number and page_number.startswith("productid"):
            page_number = page_number.replace("productid", "")

        if int(page_number) in [row[0] for row in rows]:
            response = f"Redirect ({page_number})"
        else:
            response = "Redirect Incorrect"

        dispatcher.utter_message(response)

        return []
    
class QueryAllProducts(Action):
    def name(self) -> Text:
        return "query_top"

    def run(
        self,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> List[Dict[Text, Any]]:
        if is_user_mode_customer(tracker):
            dispatcher.utter_message(text="As a customer, you do not have privileged assess to this query.")
        else:
            slot_value = next(tracker.get_latest_entity_values("top_hits"), None)
            connection = sqlite3.connect(database_path)
            cursor = connection.cursor()

            select_query = f"SELECT * FROM StoreTransaction ORDER BY Quantity DESC LIMIT {slot_value}"
            cursor.execute(select_query)
            rows = cursor.fetchall()

            rows_dict = []
            for row in rows:
                row_dict = {description[0]: value for description, value in zip(cursor.description, row)}
                rows_dict.append(row_dict)

            response_data = {"table": rows_dict}

            response_json = json.dumps(response_data)

            dispatcher.utter_message(text=response_json)

            connection.close()

        return []