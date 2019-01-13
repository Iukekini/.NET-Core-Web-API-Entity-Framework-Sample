# DOTNET parameters
DOTNETCMD=dotnet
DOTNETBUILD=$(DOTNETCMD) build
DOTNETRESTORE=$(DOTNETCMD) restore
DOTNETCLEAN=$(DOTNETCMD) clean
DOTNETTEST=$(DOTNETCMD) test
DOTNETRUN=$(DOTNETCMD) run

all: test build
build: 
	echo 'Building Projects'
	$(DOTNETRESTORE) 
	$(DOTNETBUILD) 
test: 
	echo 'Running Test'
	$(DOTNETRESTORE) 
	$(DOTNETTEST) -v=m ./Web_API_Entity_Framework_Sample_Test/Web_API_Entity_Framework_Sample_Test.csproj 

clean: 
	$(DOTNETCLEAN)
run:
	$(DOTNETRESTORE)
	$(DOTNETBUILD) 
	$(DOTNETRUN) --project ./Web_API_Entity_Framework_Sample/Web_API_Entity_Framework_Sample.csproj 

