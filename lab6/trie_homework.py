class TrieNode:
    def __init__(self):
        self.children = {}
        self.is_end_of_word = False

class Homework:
    def __init__(self):
        self.root = TrieNode()

    def put(self, word, value=None):
        if not isinstance(word, str): raise TypeError("Word must be a string")
        node = self.root
        for char in word:
            if char not in node.children:
                node.children[char] = TrieNode()
            node = node.children[char]
        node.is_end_of_word = True

    
    def has_prefix(self, prefix) -> bool:
        if not isinstance(prefix, str): return False
        node = self.root
        for char in prefix:
            if char not in node.children:
                return False
            node = node.children[char]
        return True

    def count_words_with_suffix(self, pattern) -> int:
        if not isinstance(pattern, str): return 0
        all_words = self.get_all_words()
        return sum(1 for word in all_words if word.endswith(pattern))

    def get_all_words(self) -> list:
        words = []
        def _dfs(node, current_word):
            if node.is_end_of_word:
                words.append(current_word)
            for char in sorted(node.children.keys()):
                _dfs(node.children[char], current_word + char)
        
        _dfs(self.root, "")
        return words

    def count_single_child_nodes(self) -> int:
        count = 0
        nodes_to_visit = [self.root]
        
        while nodes_to_visit:
            current = nodes_to_visit.pop()
            if len(current.children) == 1:
                count += 1
            for child in current.children.values():
                nodes_to_visit.append(child)
        return count


trie = Homework()
for word in ["apple", "application", "banana", "cat"]:
    trie.put(word)

print(f"Усі слова: {trie.get_all_words()}") # ["apple", "application", "banana", "cat"]
print(f"Вузлів з одним нащадком: {trie.count_single_child_nodes()}")
print(f"Має префікс 'app': {trie.has_prefix('app')}")