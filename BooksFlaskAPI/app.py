from flask import Flask, jsonify, request

# flask run --port 8080

app = Flask(__name__)

books = [
    { 'title': 'Dune', 'datePublished': '1970-01-01', 'name': 'Prentis Hall'},
    { 'title': 'C# For Dummies', 'datePublished': '2019-01-02', 'name': 'Dragon Press'},
    { 'title': "Surely You're Joking, Mr. Feynman!", 'datePublished': '1975-01-03', 'name': 'W. W. Norton & Company'},
    { 'title': "A Universe from Nothing", 'datePublished': '1995-01-01', 'name': 'Atria'},
    { 'title': 'C# In Depth', 'datePublished': '2008-05-01', 'name': 'Manning Publications'},
    { 'title': 'Mountain Climbing For Dummies', 'datePublished': '2018-01-01', 'name': 'Alpha Books'},
    { 'title': 'Thinking 101: How to Reason Better to Live Better', 'datePublished': '2022-09-01', 'name': 'Flatiron Books'},
    { 'title': 'The Tale of the Body Thief', 'datePublished': '2022-09-01', 'name': 'Vampire Media'},
    { 'title': 'The Exoplanet Handbook', 'datePublished': '20112-04-01', 'name': 'Cambridge University Press'},
]

@app.route("/books")
def get_books():
    return jsonify(books)

@app.route("/book/<string:title>", methods = ['GET'])
def get_bookByTitle(title):
    print(f"Search for [{title}]")
    book = {}
    for b in books:
        if title == b['title']:
            book = b
            break
    if book == {}:
        return '', 404
    return jsonify(b)

@app.route("/books", methods=['POST'])
def add_published():
    books.append(request.get_json())
    return '', 204

if __name__ == "__main__":
    app.run()