import subprocess
import os
import io
from flask import Flask
from flask import request, jsonify

app = Flask(__name__)

MY_AUTH_TOKEN = os.getenv('CI_TOKEN', None)
PATH_TO_SCRIPT = os.getenv('PATH_TO_SCRIPT', None)

@app.route('/', methods=['POST'])
def MainHandler():
    if request.headers.get('Authorization') != MY_AUTH_TOKEN:
        return jsonify({'message': 'Bad token'}), 401
    else:
        logs = []
        errors = []
        logs.append('run \'' + PATH_TO_SCRIPT + '\'')
        
        shellscript = subprocess.Popen(["sh", PATH_TO_SCRIPT], stdin=subprocess.PIPE, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
        stdout, stderr = shellscript.communicate()
        
        for line in stdout.decode("utf-8").split('\n'):
            logs.append(line)
        for line in stderr.decode("utf-8").split('\n'):
            errors.append(line)
            
        logs.append('done!')
        return jsonify({'message': 'Any work', 'logs': logs, 'errors': errors}), 200

def main():
    app.run(host='127.0.0.1', port=1114)

if __name__ == '__main__':
    main()