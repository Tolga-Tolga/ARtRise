

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityIntegration;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.ImgprocModule
{
    public partial class Subdiv2D : DisposableOpenCVObject
    {


        //
        // C++:   cv::Subdiv2D::Subdiv2D(Rect rect)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        /// <param name="rect">
        /// Rectangle that includes all of the 2D points that are to be added to the subdivision.
        /// </param>
        /// <remarks>
        ///      The function creates an empty Delaunay subdivision where 2D points can be added using the function
        ///      insert() . All of the points to be added must be within the specified rectangle, otherwise a runtime
        ///      error is raised.
        /// </remarks>
        public Subdiv2D(in Vec4i rect)
        {


            nativeObj = DisposableObject.ThrowIfNullIntPtr(imgproc_Subdiv2D_Subdiv2D_11(rect.Item1, rect.Item2, rect.Item3, rect.Item4));


        }


        //
        // C++:  void cv::Subdiv2D::initDelaunay(Rect rect)
        //

        /// <summary>
        ///  Creates a new empty Delaunay subdivision
        /// </summary>
        /// <param name="rect">
        /// Rectangle that includes all of the 2D points that are to be added to the subdivision.
        /// </param>
        public void initDelaunay(in Vec4i rect)
        {
            ThrowIfDisposed();

            imgproc_Subdiv2D_initDelaunay_10(nativeObj, rect.Item1, rect.Item2, rect.Item3, rect.Item4);


        }


        //
        // C++:  int cv::Subdiv2D::insert(Point2f pt)
        //

        /// <summary>
        ///  Insert a single point into a Delaunay triangulation.
        /// </summary>
        /// <param name="pt">
        /// Point to insert.
        /// </param>
        /// <remarks>
        ///      The function inserts a single point into a subdivision and modifies the subdivision topology
        ///      appropriately. If a point with the same coordinates exists already, no new point is added.
        /// </remarks>
        /// <returns>
        ///  the ID of the point.
        /// </returns>
        /// <remarks>
        ///      @note If the point is outside of the triangulation specified rect a runtime error is raised.
        /// </remarks>
        public int insert(in Vec2d pt)
        {
            ThrowIfDisposed();

            return imgproc_Subdiv2D_insert_10(nativeObj, pt.Item1, pt.Item2);


        }


        //
        // C++:  int cv::Subdiv2D::locate(Point2f pt, int& edge, int& vertex)
        //

        /// <summary>
        ///  Returns the location of a point within a Delaunay triangulation.
        /// </summary>
        /// <param name="pt">
        /// Point to locate.
        /// </param>
        /// <param name="edge">
        /// Output edge that the point belongs to or is located to the right of it.
        /// </param>
        /// <param name="vertex">
        /// Optional output vertex the input point coincides with.
        /// </param>
        /// <remarks>
        ///      The function locates the input point within the subdivision and gives one of the triangle edges
        ///      or vertices.
        /// </remarks>
        /// <returns>
        ///  an integer which specify one of the following five cases for point location:
        ///      -  The point falls into some facet. The function returns #PTLOC_INSIDE and edge will contain one of
        ///         edges of the facet.
        ///      -  The point falls onto the edge. The function returns #PTLOC_ON_EDGE and edge will contain this edge.
        ///      -  The point coincides with one of the subdivision vertices. The function returns #PTLOC_VERTEX and
        ///         vertex will contain a pointer to the vertex.
        ///      -  The point is outside the subdivision reference rectangle. The function returns #PTLOC_OUTSIDE_RECT
        ///         and no pointers are filled.
        ///      -  One of input arguments is invalid. A runtime error is raised or, if silent or "parent" error
        ///         processing mode is selected, #PTLOC_ERROR is returned.
        /// </returns>
        public int locate(in Vec2d pt, int[] edge, int[] vertex)
        {
            ThrowIfDisposed();
            double[] edge_out = new double[1];
            double[] vertex_out = new double[1];
            int retVal = imgproc_Subdiv2D_locate_10(nativeObj, pt.Item1, pt.Item2, edge_out, vertex_out);
            if (edge != null) edge[0] = (int)edge_out[0];
            if (vertex != null) vertex[0] = (int)vertex_out[0];
            return retVal;
        }


        //
        // C++:  int cv::Subdiv2D::findNearest(Point2f pt, Point2f* nearestPt = 0)
        //

        /// <summary>
        ///  Finds the subdivision vertex closest to the given point.
        /// </summary>
        /// <param name="pt">
        /// Input point.
        /// </param>
        /// <param name="nearestPt">
        /// Output subdivision vertex point.
        /// </param>
        /// <remarks>
        ///      The function is another function that locates the input point within the subdivision. It finds the
        ///      subdivision vertex that is the closest to the input point. It is not necessarily one of vertices
        ///      of the facet containing the input point, though the facet (located using locate() ) is used as a
        ///      starting point.
        /// </remarks>
        /// <returns>
        ///  vertex ID.
        /// </returns>
        public int findNearest(in Vec2d pt, out Vec2d nearestPt)
        {
            ThrowIfDisposed();
            double[] nearestPt_out = new double[2];
            int retVal = imgproc_Subdiv2D_findNearest_10(nativeObj, pt.Item1, pt.Item2, nearestPt_out);
            { nearestPt.Item1 = nearestPt_out[0]; nearestPt.Item2 = nearestPt_out[1]; }
            return retVal;
        }

        /// <summary>
        ///  Finds the subdivision vertex closest to the given point.
        /// </summary>
        /// <param name="pt">
        /// Input point.
        /// </param>
        /// <param name="nearestPt">
        /// Output subdivision vertex point.
        /// </param>
        /// <remarks>
        ///      The function is another function that locates the input point within the subdivision. It finds the
        ///      subdivision vertex that is the closest to the input point. It is not necessarily one of vertices
        ///      of the facet containing the input point, though the facet (located using locate() ) is used as a
        ///      starting point.
        /// </remarks>
        /// <returns>
        ///  vertex ID.
        /// </returns>
        public int findNearest(in Vec2d pt)
        {
            ThrowIfDisposed();

            return imgproc_Subdiv2D_findNearest_11(nativeObj, pt.Item1, pt.Item2);


        }


        //
        // C++:  Point2f cv::Subdiv2D::getVertex(int vertex, int* firstEdge = 0)
        //

        /// <summary>
        ///  Returns vertex location from vertex ID.
        /// </summary>
        /// <param name="vertex">
        /// vertex ID.
        /// </param>
        /// <param name="firstEdge">
        /// Optional. The first edge ID which is connected to the vertex.
        /// </param>
        /// <returns>
        ///  vertex (x,y)
        /// </returns>
        public Vec2d getVertexAsVec2d(int vertex, int[] firstEdge)
        {
            ThrowIfDisposed();
            double[] firstEdge_out = new double[1];
            double[] tmpArray = new double[2];
            imgproc_Subdiv2D_getVertex_10(nativeObj, vertex, firstEdge_out, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);
            if (firstEdge != null) firstEdge[0] = (int)firstEdge_out[0];
            return retVal;
        }

        /// <summary>
        ///  Returns vertex location from vertex ID.
        /// </summary>
        /// <param name="vertex">
        /// vertex ID.
        /// </param>
        /// <param name="firstEdge">
        /// Optional. The first edge ID which is connected to the vertex.
        /// </param>
        /// <returns>
        ///  vertex (x,y)
        /// </returns>
        public Vec2d getVertexAsVec2d(int vertex)
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            imgproc_Subdiv2D_getVertex_11(nativeObj, vertex, tmpArray);
            Vec2d retVal = new Vec2d(tmpArray[0], tmpArray[1]);

            return retVal;
        }


        //
        // C++:  int cv::Subdiv2D::edgeOrg(int edge, Point2f* orgpt = 0)
        //

        /// <summary>
        ///  Returns the edge origin.
        /// </summary>
        /// <param name="edge">
        /// Subdivision edge ID.
        /// </param>
        /// <param name="orgpt">
        /// Output vertex location.
        /// </param>
        /// <returns>
        ///  vertex ID.
        /// </returns>
        public int edgeOrg(int edge, out Vec2d orgpt)
        {
            ThrowIfDisposed();
            double[] orgpt_out = new double[2];
            int retVal = imgproc_Subdiv2D_edgeOrg_10(nativeObj, edge, orgpt_out);
            { orgpt.Item1 = orgpt_out[0]; orgpt.Item2 = orgpt_out[1]; }
            return retVal;
        }


        //
        // C++:  int cv::Subdiv2D::edgeDst(int edge, Point2f* dstpt = 0)
        //

        /// <summary>
        ///  Returns the edge destination.
        /// </summary>
        /// <param name="edge">
        /// Subdivision edge ID.
        /// </param>
        /// <param name="dstpt">
        /// Output vertex location.
        /// </param>
        /// <returns>
        ///  vertex ID.
        /// </returns>
        public int edgeDst(int edge, out Vec2d dstpt)
        {
            ThrowIfDisposed();
            double[] dstpt_out = new double[2];
            int retVal = imgproc_Subdiv2D_edgeDst_10(nativeObj, edge, dstpt_out);
            { dstpt.Item1 = dstpt_out[0]; dstpt.Item2 = dstpt_out[1]; }
            return retVal;
        }

    }
}
