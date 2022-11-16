

# @see http://marmelab.com/blog/2016/02/29/auto-documented-makefile.html
.DEFAULT_GOAL := help
.PHONY: help
help: ## provides cli help for this makefile (default) 📖
	@grep -E '^[a-zA-Z_0-9-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'
	
.PHONY: init-git-remote-repo
init-git-remote-repo: add-origin add-github add-git-all
	
.PHONY: add-origin # add git origin url
add-origin: 	
	git remote add origin ssh://mfiari@80.93.90.133:2222/data/git/gmm/gmm_2d.git
	
.PHONY: add-github # add github url
add-github: 	
	git remote add github https://github.com/mfiari-GMM/gmm2d.git
	
.PHONY: add-git-all # add origin and github url to all
add-git-all: 	
	git remote add all ssh://mfiari@80.93.90.133:2222/data/git/gmm/gmm_2d.git
	git remote set-url --add all https://github.com/mfiari-GMM/gmm2d.git







