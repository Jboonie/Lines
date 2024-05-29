import os,sys
import toml


def load_config(config_file):
    with open(config_file, 'r') as file:
        return toml.load(file)

def get_files(directory='.\\', extension='.cs'):
    matching_files = []
    for root, directories, files in os.walk(directory):
        for filename in files:
            if filename.endswith(extension):
                filepath = os.path.abspath(os.path.join(root, filename))
                matching_files.append(filepath)
    return matching_files

def get_spaces(line):
    spaces = len(line) - len(line.lstrip())
    return ' ' * spaces

def process_file(file, insert_after, insert_data):
    with open(file, "r") as in_file:
        buffer = in_file.readlines()

    modified_buffer = []
    contents = ''.join(buffer)
    if insert_after in contents and insert_data not in contents:
        for line in buffer:
            if insert_after in line:
                line = f'{line}{get_spaces(line) + insert_data}\n'
            modified_buffer.append(line)
    else:
        modified_buffer = buffer

    with open(file, "w") as out_file:
        out_file.writelines(modified_buffer)

def main():
    config_file = './lines.toml'
    config = load_config(config_file)

    insert_after = config['Target']['InsertAfter']
    insert_data = config['Payload']['insertData']

    print(f'Searching for this string to insert after: {insert_after}')
    print(f'Planning to insert this data: {insert_data}')

    files = get_files()

    print(f'\nThese files will be modified:')
    for file in files:
        print(f'\t{file}')

    print('\nPress ENTER to continue, press Ctrl + C to stop')
    userInput = input().lower()

    for file in files:
        process_file(file, insert_after, insert_data)

if __name__ == "__main__":
    main()