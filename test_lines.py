import os
import shutil
import unittest
import toml

# Import the functions from your code file
from lines import load_config, get_files, get_spaces, process_file

class TestCodeFunctions(unittest.TestCase):
    def setUp(self):
        # Create copies of the template files in the \tests\ folder
        shutil.copy('.\\tests\\templates\\missing.cs', '.\\tests\\missing.cs')
        shutil.copy('.\\tests\\templates\\contains.cs', '.\\tests\\contains.cs')

    def tearDown(self):
        # Remove the copied files from the \tests\ folder
        os.remove('.\\tests\\missing.cs')
        os.remove('.\\tests\\contains.cs')

    def test_load_config(self):
        config_file = './lines.toml'
        config = load_config(config_file)
        self.assertIsInstance(config, dict)
        self.assertIn('Target', config, 'test')
        self.assertIn('Payload', config)
        print('test_load_config - Success')

    def test_get_files(self):
        files = get_files(directory='.\\tests\\', extension='.cs')
        self.assertEqual(len(files), (4))
        self.assertIn(os.path.abspath('.\\tests\\missing.cs'), files)
        self.assertIn(os.path.abspath('.\\tests\\contains.cs'), files)
        print('test_get_files - Success')

    def test_get_spaces(self):
        line = '    test line'
        spaces = get_spaces(line)
        self.assertEqual(spaces, '    ')
        print('test_get_spaces - Success')

    def test_process_file_missing(self):
        file = '.\\tests\\missing.cs'
        insert_after = 'base.Initialize();'
        insert_data = 'this.GetComponent<LinkComponent>().Initialize(20);'
        process_file(file=file, insert_after=insert_after, insert_data=insert_data)
        with open(file, 'r') as f:
            contents = f.read()
            self.assertIn(insert_after, contents)
            self.assertIn(insert_data, contents)
        print('test_process_file_missing - Success')

    def test_process_file_contains(self):
        file = '.\\tests\\contains.cs'
        insert_after = 'base.Initialize();'
        insert_data = 'this.GetComponent<LinkComponent>().Initialize(20);'
        process_file(file, insert_after, insert_data)
        with open(file, 'r') as f:
            contents = f.read()
            self.assertIn(insert_after, contents)
            
            # Check if there are no other occurrences of insert_data
            self.assertEqual(contents.count(insert_data), 1)
        print('test_process_file_contains - Success')

if __name__ == '__main__':
    unittest.main()