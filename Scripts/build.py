import argparse, sys, os
 
def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('-p', '--project', type=str, required=True, help="Project name to build")
    parser.add_argument('-b', '--build', type=int, required=True, help="Build number to append to versioning")
    parser.add_argument('-c', '--config', type=str, default="Debug", help="Debug|Release defaults to Debug")
    parser.add_argument('-o', '--output', type=str, help="Output folder")
    args = parser.parse_args()

    build(args.project, args.build, args.config, args.output)

def build(project, build_number, config="Debug", output=None):
    path = "../{0}/{0}.csproj".format(project)

    if(output == None or output == ""):
        output = "../bin/builds/{}/{}".format(project, config)

    version = "{}.{}".format(getVersion(project), build_number)

    print("Building '{}' '{}' '{}' to output '{}'".format(project, config, version, output))

    os.system("dotnet clean {} --configuration {} --output {} --verbosity n".format(path, config, output))
    os.system("dotnet publish {} --configuration {} --output {} --verbosity n /property:Version={}".format(path, config, output, version))

def getVersion(project):
    path = "../{0}/version".format(project)

    with open(path) as f:
        return f.readline().replace("\n", "").replace("\r", "").replace("v", "")

if __name__ == "__main__":
    main()