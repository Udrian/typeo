cmake_minimum_required (VERSION 3.15)

set(PROJECT_NAME    "Box2D")
set(PROJECT_VERSION "0.1.0.0")
set(DOTNET_VERSION  "v4.7.2")
set(LANG_VERSION    "/langversion:7.3")
set(PROJECT_REFERENCES
	"System"
)
file(GLOB PROJECT_SRC
	"Collision/Shapes/CircleShape.cs"
	"Collision/Shapes/EdgeShape.cs"
	"Collision/Shapes/LoopShape.cs"
	"Collision/Shapes/PolygonShape.cs"
	"Collision/Shapes/Shape.cs"
	"Collision/BroadPhase.cs"
	"Collision/Collision.cs"
	"Collision/Distance.cs"
	"Collision/DynamicTree.cs"
	"Collision/TimeOfImpact.cs"
	"Common/FixedArray.cs"
	"Common/Math.cs"
	"Common/Settings.cs"
	"Dynamics/Contacts/Contact.cs"
	"Dynamics/Contacts/ContactSolver.cs"
	"Dynamics/Contacts/TOISolver.cs"
	"Dynamics/Joints/DistanceJoint.cs"
	"Dynamics/Joints/FrictionJoint.cs"
	"Dynamics/Joints/GearJoint.cs"
	"Dynamics/Joints/Joint.cs"
	"Dynamics/Joints/LineJoint.cs"
	"Dynamics/Joints/MaxDistanceJoint.cs"
	"Dynamics/Joints/MouseJoint.cs"
	"Dynamics/Joints/PrismaticJoint.cs"
	"Dynamics/Joints/PullyJoint.cs"
	"Dynamics/Joints/RevoluteJoint.cs"
	"Dynamics/Joints/WeldJoint.cs"
	"Dynamics/Body.cs"
	"Dynamics/ContactManager.cs"
	"Dynamics/Fixture.cs"
	"Dynamics/Island.cs"
	"Dynamics/TimeStep.cs"
	"Dynamics/World.cs"
	"Dynamics/WorldCallbacks.cs"
)


project(${PROJECT_NAME} VERSION ${PROJECT_VERSION} LANGUAGES CSharp)

set(CMAKE_CSharp_FLAGS ${LANG_VERSION})

add_library(${PROJECT_NAME} SHARED ${PROJECT_SRC})

set_property( TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION ${DOTNET_VERSION} )
set_property( TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_REFERENCES ${PROJECT_REFERENCES} )

add_dependencies(${PROJECT_NAME} "TypeOCommon")